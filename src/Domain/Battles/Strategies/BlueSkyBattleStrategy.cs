using System.Collections.Immutable;
using System.ComponentModel;
using Domain.ActivationRules;
using Domain.BattlePlans;
using Domain.Battles.Events;
using Domain.Battles.Spheres;
using Domain.MagicCards;
using Domain.Warriors;

namespace Domain.Battles.Strategies;

public sealed class BlueSkyBattleStrategy(
IMagicCardStrategyFactory magicCardStrategy,
IFightDecisionSource decisionSource,
IActivationRuleEvaluatorSelector ruleEvaluator,
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

    private void Attack(Warrior attacker, Warrior opponent)
    {
        TryToApplyMagic(new AttackContext(attacker, opponent));

        int damage = decisionSource.PickBaseDamage(attacker.MaxDamage);
        attacker.Hit(damage, opponent);
        attacker.CourseBites();

        RecordEvent(new AttackLanded(attacker, opponent, damage));

        void TryToApplyMagic(AttackContext attackContext)
        {
            var rulesValidated = attackContext.Attacker.TryEvaluateRules(attackContext, out var slot);

            if (!rulesValidated)
            {
                int slotIndex = decisionSource.PickSlotIndex(CardDrawAttemptRangeMax);
                attackContext.Attacker.TryToTouchSlot(slotIndex, out slot);
            }

            if (slot is null)
            {
                return;
            }

            if (rulesValidated)
            {
                ApplyMagic();
                return;
            }

            if (!ruleEvaluator.SelectBy(slot.Rule).Matches(slot.Rule, attackContext))
            {
                return;
            }

            ApplyMagic();

            void ApplyMagic()
            {                
                RecordEvent(new CardDrawn(attackContext.Attacker, slot.Card.Name));

                magicCardStrategy
                    .SelectBy(slot.Card)
                    .ApplyMagic(attackContext.Attacker, attackContext.Opponent, slot.Card);
            }
        }
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
