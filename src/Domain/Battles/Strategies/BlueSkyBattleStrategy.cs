using System.Collections.Immutable;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using Domain.Battles.Events;
using Domain.Battles.Spheres;
using Domain.MagicCards;
using Domain.Shared;
using Domain.Warriors;

namespace Domain.Battles.Strategies;

public sealed class BlueSkyBattleStrategy(
IMagicCardStrategyFactory magicCardStrategy,
IFightDecisionSource decisionSource) : BattleStrategyBase<BlueSkysphere>
{
    private const int CardDrawAttemptRangeMax = 15;
    private readonly List<IBattleEvent> _battleEvents = [];

    public override BattleResult StartBattle(BattleContext battleContext)
    {
        ArgumentNullException.ThrowIfNull(battleContext);

        battleContext.CheckIfCompetitiorsAreWithinTheSameSphere();

        _battleEvents.Clear();

        RecordEvent(new BattleStarted(battleContext.Attacker, battleContext.Opponent));
        for (int round = 0; round < battleContext.RoundsCount; round++)
        {
            RecordEvent(new RoundStarted(round + 1));

            Fight(battleContext.Attacker, battleContext.Opponent);
            Fight(battleContext.Opponent, battleContext.Attacker);

            bool isLastRound = battleContext.RoundsCount == round + 1;

            if (TryBuildEndEvent(battleContext, isLastRound) is { } evt)
            {
                RecordEvent(evt);
                return Emit();
            }
        }

        throw new InvalidOperationException("Battle did not produce an end event.");
    }

    private IBattleEvent? TryBuildEndEvent(BattleContext ctx, bool isLastRound)
    {
        if (ctx.CheckDoubleKnockout())
        {
            return new DoubleKnockoutOccurred(ctx.Attacker, ctx.Opponent);
        }

        if (ctx.TryGetDeath(out var outcome))
        {
            return new WarriorDied(outcome.Dead, outcome.Survivor);
        }

        if (!isLastRound)
        {
            return null;
        }

        if (ctx.Attacker.Health > ctx.Opponent.Health)
        {
            return new BattleFinished(ctx.Attacker, ctx.Opponent);
        }

        if (ctx.Attacker.Health < ctx.Opponent.Health)
        {
            return new BattleFinished(ctx.Opponent, ctx.Attacker);
        }

        return new BattleFinishedTied(ctx.Attacker, ctx.Opponent);
    }

    private void RecordEvent(IBattleEvent @event)
    {
        _battleEvents.Add(@event);
    }

    private BattleResult Emit()
    {
        var events = ImmutableArray.CreateRange(_battleEvents);        
        _battleEvents.Clear();
        return new BattleResult(events);
    }

    private void Fight(Warrior attacker, Warrior oponent)
    {
        int cardIndex = decisionSource.PickCardIndex(CardDrawAttemptRangeMax);

        DrawResult drawResult = attacker.TryToDrawCard(cardIndex);

        if (drawResult.Card is { } card && decisionSource.TryActivate(card.ActivationChance))
        {
            RecordEvent(new CardDrawn(attacker, card.Name));

            magicCardStrategy
                .SelectBy(card)
                .ApplyMagic(attacker, oponent, card);
        }

        int damage = decisionSource.PickBaseDamage(attacker.MaxDamage);
        attacker.Hit(damage, oponent);
        attacker.CourseBites();

        _battleEvents.Add(new AttackLanded(attacker, oponent, damage));
    }
}
