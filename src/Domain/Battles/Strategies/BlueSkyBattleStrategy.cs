using System.Collections.Immutable;
using Domain.ActivationRules;
using Domain.Battles.Events;
using Domain.Battles.Spheres;
using Domain.MagicCards;
using Domain.Warriors;

namespace Domain.Battles.Strategies;

public sealed class BlueSkyBattleStrategy(
IMagicCardStrategyFactory magicCardStrategy,
IFightDecisionSource decisionSource,
IActivationRuleEvaluatorSelector cardActivator,
IBattleEndEventBuilder battleEndEventBuilder) : BattleStrategyBase<BlueSkysphere>
{
    private const int CardDrawAttemptRangeMax = 15;
    private readonly List<IBattleEvent> _battleEvents = [];

    public override BattleResult StartBattle(BattleContext battleContext, DateTimeOffset startedAt)
    {
        RecordEvent(new BattleStarted(battleContext.Attacker, battleContext.Opponent, startedAt));
        for (int round = 0; round < battleContext.RoundsCount; round++)
        {
            RecordEvent(new RoundStarted(round + 1));

            Attack(battleContext.Attacker, battleContext.Opponent);
            Attack(battleContext.Opponent, battleContext.Attacker);

            RecordEvent(new RoundStatsCaptured(battleContext.Attacker, battleContext.Opponent));

            bool isLastRound = battleContext.RoundsCount == round + 1;

            if (battleEndEventBuilder.TryBuildEndEvent(battleContext, isLastRound) is { } evt)
            {
                RecordEvent(evt);
                return Emit();
            }
        }

        throw new InvalidOperationException("Battle did not produce an end event.");
    }

    private void RecordEvent(IBattleEvent @event)
    {
        @event.SetOrder(_battleEvents.Count);
        _battleEvents.Add(@event);
    }

    private BattleResult Emit()
    {
        var events = ImmutableArray.CreateRange(_battleEvents);
        _battleEvents.Clear();
        return BattleResult.Create(events);
    }

    private void Attack(Warrior attacker, Warrior oponent)
    {
        int cardIndex = decisionSource.PickCardIndex(CardDrawAttemptRangeMax);

        SlotResult touchResult = attacker.TryToTouchSlot(cardIndex);

        if (touchResult.Slot is { } slot && cardActivator.SelectBy(slot.Rule).Matches(slot.Rule, new AttackContext(attacker, oponent)))
        {
            RecordEvent(new CardDrawn(attacker, slot.Card.Name));

            magicCardStrategy
                .SelectBy(slot.Card)
                .ApplyMagic(attacker, oponent, slot.Card);
        }

        int damage = decisionSource.PickBaseDamage(attacker.MaxDamage);
        attacker.Hit(damage, oponent);
        attacker.CourseBites();

        RecordEvent(new AttackLanded(attacker, oponent, damage));
    }
}

public interface IBattleEndEventBuilder
{
    IBattleEvent? TryBuildEndEvent(BattleContext ctx, bool isLastRound);
}

public sealed class BattleEndEventBuilder : IBattleEndEventBuilder
{
    public IBattleEvent? TryBuildEndEvent(BattleContext ctx, bool isLastRound)
    {
        if (ctx.TryGetDeath(out var _) == DeathState.Double)
        {
            return new DoubleKnockoutOccurred(ctx.Attacker, ctx.Opponent);
        }

        if (ctx.TryGetDeath(out var outcome) == DeathState.Single)
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
}
