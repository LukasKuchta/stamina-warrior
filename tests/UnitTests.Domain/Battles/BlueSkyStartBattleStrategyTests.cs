using System.Collections.Immutable;
using Domain.ActivationRules;
using Domain.Battles;
using Domain.Battles.Events;
using Domain.Battles.Strategies;
using Domain.MagicCards;
using Domain.MagicCards.Cards;
using Domain.MagicCards.Rules;
using Domain.MagicCards.Strategies;
using Domain.Shared;
using Domain.UnitTests.Battles.Shared;
using Domain.Warriors.Events;
using Shouldly;

namespace Domain.UnitTests.Battles;

public class BlueSkyStrategBattleResultsyTests
{
    [Fact]
    public void StartBattle_RoundStatsCapture_ShouldHaveValues()
    {
        var conan = WarriorHelper.CreateBlueSky("Connan", 1);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 1);

        var blueSkyStrategy = CreateBluSkyStrategy();

        var battleResultBase = blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 1), Time.StartBattleAt);

        var battleResult = battleResultBase.ShouldBeAssignableTo<BattleResult>();

        battleResult.BattleEvents.ShouldNotBeEmpty();
        var evt = battleResult.BattleEvents[4].ShouldBeOfType<RoundStatsCaptured>();

        evt.Attacker.Name.ShouldBe(conan.Name);
        evt.Opponent.Name.ShouldBe(brutus.Name);

        evt.Attacker.Health.ShouldBe(75);
        evt.Opponent.Health.ShouldBe(75);
    }

    [Fact]
    public void StartBattle_BattleShouldWin_Connan()
    {
        var conan = WarriorHelper.CreateBlueSky("Connan", 3);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 1);

        var blueSkyStrategy = CreateBluSkyStrategy();

        var battleResultBase = blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 1), Time.StartBattleAt);

        var battleResult = battleResultBase.ShouldBeAssignableTo<BattleResult>();

        battleResult.BattleEvents.ShouldNotBeEmpty();
        var lastEvent = battleResult.BattleEvents[battleResult.BattleEvents.Length - 1].ShouldBeOfType<BattleFinished>();

        lastEvent.Winner.Name.ShouldBe(conan.Name);
        lastEvent.Looser.Name.ShouldBe(brutus.Name);
    }

    [Fact]
    public void StartBattle_BattleResult_ShouldHaveIncreasingOrder()
    {
        var conan = WarriorHelper.CreateBlueSky("Connan", 3);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 1);

        var blueSkyStrategy = CreateBluSkyStrategy();

        var battleResultBase = blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 1), Time.StartBattleAt);

        var battleResult = battleResultBase.ShouldBeAssignableTo<BattleResult>();

        for (int i = 0; i < battleResult.BattleEvents.Length - 1; i++)
        {
            battleResult.BattleEvents[i].Order.ShouldBe(i);
        }
    }

    [Fact]
    public void StartBattle_BattleShouldHas3Iterations()
    {
        var conan = WarriorHelper.CreateBlueSky("Connan", 2);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 2);

        var recorder = new RecordingEndEventBuilder();
        var blueSkyStrategy = new BlueSkyBattleStrategy(
            new MagicCardStrategyFactory([]),
            new EchoDecisionSource(0),
            new ActivationRuleEvaluatorSelector([]),
            recorder);

        Action act = () => blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 3), Time.StartBattleAt);

        var ex = act.ShouldThrow<InvalidOperationException>();
        ex.Message.ShouldNotBeNullOrEmpty();

        recorder.Flags.Count.ShouldBe(3);
    }

    [Fact]
    public void StartBattle_ShouldWin_Brutus()
    {
        var conan = WarriorHelper.CreateBlueSky("Connan", 1);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 3);

        var blueSkyStrategy = CreateBluSkyStrategy();

        var battleResultBase = blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 1), Time.StartBattleAt);

        var battleResult = battleResultBase.ShouldBeAssignableTo<BattleResult>();

        battleResult.BattleEvents.ShouldNotBeEmpty();
        var lastEvent = battleResult.BattleEvents[battleResult.BattleEvents.Length - 1].ShouldBeOfType<BattleFinished>();

        lastEvent.Winner.Name.ShouldBe(brutus.Name);
        lastEvent.Looser.Name.ShouldBe(conan.Name);
    }

    [Fact]
    public void StartBattle_BattleShouldSurvive_Connan()
    {
        var conan = WarriorHelper.CreateBlueSky("Connan", 300);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 1);

        var blueSkyStrategy = CreateBluSkyStrategy();

        var battleResultBase = blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 1), Time.StartBattleAt);

        var battleResult = battleResultBase.ShouldBeAssignableTo<BattleResult>();

        battleResult.BattleEvents.ShouldNotBeEmpty();
        var lastEvent = battleResult.BattleEvents[battleResult.BattleEvents.Length - 1].ShouldBeOfType<WarriorDied>();

        lastEvent.Survivor.Name.ShouldBe(conan.Name);
        lastEvent.DeadMan.Name.ShouldBe(brutus.Name);

        lastEvent.DeadMan.Health.ShouldBeLessThan<int>(0);
    }

    [Fact]
    public void StartBattle_BattleShouldEnd_Tie()
    {
        var conan = WarriorHelper.CreateBlueSky("Conan", 1);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 1);

        var blueSkyStrategy = CreateBluSkyStrategy();

        var battleResult = blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 1), Time.StartBattleAt);

        battleResult.BattleEvents.ShouldNotBeEmpty();
        var result = battleResult.BattleEvents[battleResult.BattleEvents.Length - 1].ShouldBeOfType<BattleFinishedTied>();

        result.Warrior1Name.ShouldNotBeNullOrEmpty();
        result.Warrior2Name.ShouldNotBeNullOrEmpty();
        result.Warrior1Name.ShouldBe(conan.Name);
        result.Warrior2Name.ShouldBe(brutus.Name);
    }

    [Fact]
    public void StartBattle_BattleShouldEnd_DoubleKo()
    {
        var conan = WarriorHelper.CreateBlueSky("Connan", 1);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 1);

        var blueSkyStrategy = CreateBluSkyStrategy();

        var battleResult = blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 10), Time.StartBattleAt);

        battleResult.BattleEvents.ShouldNotBeEmpty();
        var result = battleResult.BattleEvents[battleResult.BattleEvents.Length - 1].ShouldBeOfType<DoubleKnockoutOccurred>();

        result.Attacker.ShouldNotBeNull();
        result.Oponent.ShouldNotBeNull();

        result.Attacker.Name.ShouldBe(conan.Name);
        result.Oponent.Name.ShouldBe(brutus.Name);

        result.Attacker.ToString().ShouldNotBeNullOrEmpty();
        result.Oponent.ToString().ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public void StartBattle_DrawnCardShouldViolate_CardIndexCannotBeNegativeRule()
    {
        var cardSlot = SlotHelper.Create(new HealingCard(Power.FromValue(1)));

        var conan = WarriorHelper.CreateBlueSky("Connan", 1, [cardSlot]);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 3);

        var blueSkyStrategy = new BlueSkyBattleStrategy(
            new MagicCardStrategyFactory(
                [
                    new HealingCardStrategy(),
                ]),
            new EchoDecisionSource(-1),
            new ActivationRuleEvaluatorSelector([new ChanceActivationRuleEvaluator(new FakeIRandomSource())]),
            new BattleEndEventBuilder());

        Action act = () => blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 1), Time.StartBattleAt);
        var ex = act.ShouldThrow<BusinessRuleValidationException>();
        var rule = ex.BrokenRule.ShouldBeOfType<SlotIndexCannotBeNegativeRule>();
        rule.Message.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public void StartBattle_ConanShouldDrawn_HealthCard()
    {
        var slot = SlotHelper.Create(new HealingCard(Power.FromValue(1)));

        var conan = WarriorHelper.CreateBlueSky("Connan", 1, [slot]);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 3);

        var blueSkyStrategy = new BlueSkyBattleStrategy(
            new MagicCardStrategyFactory(
                [
                    new HealingCardStrategy(),
                ]),
            new EchoDecisionSource(0),
            new ActivationRuleEvaluatorSelector([new ChanceActivationRuleEvaluator(new FakeIRandomSource())]),
            new BattleEndEventBuilder());

        var battleResult = blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 1), Time.StartBattleAt);

        battleResult.BattleEvents.ShouldNotBeEmpty();
        var healingCardDrawn = battleResult.BattleEvents[2].ShouldBeOfType<CardDrawn>();
        healingCardDrawn.CardName.ShouldBe(slot.Card.Name);
        healingCardDrawn.CardHolder.ShouldBe(conan.Name);
    }

    [Fact]
    public void StartBattle_ConanShouldDrawn_ThornCard()
    {
        var slot = SlotHelper.Create(new ThornDamageCard(Power.FromValue(1)));

        var conan = WarriorHelper.CreateBlueSky("Connan", 1, [slot]);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 3);

        var blueSkyStrategy = new BlueSkyBattleStrategy(
            new MagicCardStrategyFactory(
                [
                    new ThornDamageStrategy(),
                ]),
            new EchoDecisionSource(0),
            new ActivationRuleEvaluatorSelector([new ChanceActivationRuleEvaluator(new FakeIRandomSource())]),
            new BattleEndEventBuilder());

        var battleResult = blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 1), Time.StartBattleAt);
        battleResult.BattleEvents.ShouldNotBeEmpty();

        var @event = battleResult.BattleEvents[2].ShouldBeOfType<CardDrawn>();
        @event.CardName.ShouldBe(slot.Card.Name);
        @event.CardHolder.ShouldBe(conan.Name);
    }

    [Fact]
    public void StartBattle_ConanShouldBeSelfHitted()
    {
        var thornCard = SlotHelper.Create(new ThornDamageCard(Power.FromValue(2)));
        var fightingCard = SlotHelper.Create(new FightingCard(Power.Zero));

        var conan = WarriorHelper.CreateBlueSky("Connan", 1, [thornCard]);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 1, [fightingCard]);

        var blueSkyStrategy = new BlueSkyBattleStrategy(
            new MagicCardStrategyFactory(
                [
                    new ThornDamageStrategy(),
                    new FightingCardStrategy()
                ]),
            new EchoDecisionSource(0),
            new ActivationRuleEvaluatorSelector([new ChanceActivationRuleEvaluator(new FakeIRandomSource())]),
            new BattleEndEventBuilder());

        _ = blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 1), Time.StartBattleAt);

        conan.Health.ShouldBe(50);
    }

    [Fact]
    public void StartBattle_ConanShouldDrawn_CoursedCard()
    {
        var slot = SlotHelper.Create(new CoursedCard(Power.FromValue(2)));

        var conan = WarriorHelper.CreateBlueSky("Connan", 1, [slot]);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 3);

        var blueSkyStrategy = new BlueSkyBattleStrategy(
            new MagicCardStrategyFactory(
                [
                    new CoursedCardStrategy(),
                ]),
            new EchoDecisionSource(0),
            new ActivationRuleEvaluatorSelector([new ChanceActivationRuleEvaluator(new FakeIRandomSource())]),
            new BattleEndEventBuilder());

        var battleResult = blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 2), Time.StartBattleAt);

        battleResult.BattleEvents.ShouldNotBeEmpty();
        var drawn = battleResult.BattleEvents[2].ShouldBeOfType<CardDrawn>();
        drawn.CardName.ShouldBe(slot.Card.Name);
        drawn.CardHolder.ShouldBe(conan.Name);

        brutus.Course.HasPower.ShouldBeTrue();
    }

    [Fact]
    public void StartBattle_CourseBites()
    {
        var coursedCard = SlotHelper.Create(new CoursedCard(Power.FromValue(1)));

        var conan = WarriorHelper.CreateBlueSky("Connan", 1, [coursedCard]);
        var brutus = WarriorHelper.CreateBlueSky("Brutus");

        var blueSkyStrategy = new BlueSkyBattleStrategy(
            new MagicCardStrategyFactory(
                [
                    new CoursedCardStrategy(),
                    new FightingCardStrategy()
                ]),
            new EchoDecisionSource(0),
            new ActivationRuleEvaluatorSelector([new ChanceActivationRuleEvaluator(new FakeIRandomSource())]),
            new BattleEndEventBuilder());

        _ = blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 1), Time.StartBattleAt);

        brutus.Health.ShouldBe(0);
    }

    [Fact]
    public void StartBattle_CourseDoeasntBite()
    {
        var coursedCard = SlotHelper.Create(new CoursedCard(Power.Zero));

        var conan = WarriorHelper.CreateBlueSky("Connan", 1, [coursedCard]);
        var brutus = WarriorHelper.CreateBlueSky("Brutus");

        var blueSkyStrategy = new BlueSkyBattleStrategy(
            new MagicCardStrategyFactory(
                [
                    new CoursedCardStrategy(),
                ]),
            new EchoDecisionSource(0),
            new ActivationRuleEvaluatorSelector([new ChanceActivationRuleEvaluator(new FakeIRandomSource())]),
            new BattleEndEventBuilder());

        _ = blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 1), Time.StartBattleAt);

        brutus.Health.ShouldBe(75);
    }

    [Fact]
    public void StartBattle_ConanShouldDrawn_StealingCard()
    {
        var slot1 = SlotHelper.Create(new StealingCard());
        var slot2 = SlotHelper.Create(new FightingCard(Power.FromValue(2)));

        var conan = WarriorHelper.CreateBlueSky("Connan", 1, [slot1]);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 3, [slot2]);

        var blueSkyStrategy = new BlueSkyBattleStrategy(
            new MagicCardStrategyFactory(
                [
                    new StealingCardStrategy(new FakeIRandomSource()),
                    new FightingCardStrategy()
                ]),
            new EchoDecisionSource(0),
            new ActivationRuleEvaluatorSelector([new ChanceActivationRuleEvaluator(new FakeIRandomSource())]),
            new BattleEndEventBuilder());

        var battleResult = blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 2), Time.StartBattleAt);

        battleResult.BattleEvents.ShouldNotBeEmpty();
        var drawn1 = battleResult.BattleEvents[2].ShouldBeOfType<CardDrawn>();
        drawn1.CardName.ShouldBe(slot1.Card.Name);
        drawn1.CardHolder.ShouldBe(conan.Name);

        var drawn2 = battleResult.BattleEvents[7].ShouldBeOfType<CardDrawn>();
        drawn2.CardName.ShouldBe(slot2.Card.Name);
        drawn2.CardHolder.ShouldBe(conan.Name);

        ImmutableArray<DomainEventBase> events = conan.DequeueDomainEvents();
        events.Length.ShouldBe(2);
        var card = events[0].ShouldBeOfType<CardStolen>();
        card.CardName.ShouldBe(slot2.Card.Name);
    }

    [Fact]
    public void StartBattle_BrutusShouldBeKilledByCourse()
    {
        var coursedCard = SlotHelper.Create(new CoursedCard(Power.FromValue(2)));

        var conan = WarriorHelper.CreateBlueSky("Connan", 1, [coursedCard]);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 3);

        var blueSkyStrategy = new BlueSkyBattleStrategy(
            new MagicCardStrategyFactory(
                [
                    new CoursedCardStrategy(),
                ]),
            new EchoDecisionSource(0),
            new ActivationRuleEvaluatorSelector([new ChanceActivationRuleEvaluator(new FakeIRandomSource())]),
            new BattleEndEventBuilder());

        var battleResult = blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 2), Time.StartBattleAt);

        battleResult.BattleEvents.ShouldNotBeEmpty();

        var lastEvent = battleResult.BattleEvents[battleResult.BattleEvents.Length - 1].ShouldBeOfType<WarriorDied>();
        lastEvent.Survivor.Name.ShouldBe(conan.Name);
        lastEvent.DeadMan.Name.ShouldBe(brutus.Name);
    }

    [Fact]
    public void StartBattle_BrutusShouldBeDeadMan()
    {
        var conan = WarriorHelper.CreateBlueSky("Connan", 2);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 1);

        var blueSkyStrategy = new BlueSkyBattleStrategy(
            new MagicCardStrategyFactory([]),
            new EchoDecisionSource(0),
            new ActivationRuleEvaluatorSelector([new ChanceActivationRuleEvaluator(new FakeIRandomSource())]),
            new BattleEndEventBuilder());

        var battleResult = blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 3), Time.StartBattleAt);

        battleResult.BattleEvents.ShouldNotBeEmpty();

        var lastEvent = battleResult.BattleEvents[battleResult.BattleEvents.Length - 1].ShouldBeOfType<WarriorDied>();
        lastEvent.Survivor.Name.ShouldBe(conan.Name);
        lastEvent.Survivor.Health.ShouldBeGreaterThan<int>(0);

        lastEvent.DeadMan.Name.ShouldBe(brutus.Name);
        lastEvent.DeadMan.Health.ShouldBe<int>(0);
    }

    [Fact]
    public void StartBattle_ConanShouldBeDeadMan()
    {
        var conan = WarriorHelper.CreateBlueSky("Connan");
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 2);

        var blueSkyStrategy = CreateBluSkyStrategy();

        var battleResult = blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 3), Time.StartBattleAt);

        battleResult.BattleEvents.ShouldNotBeEmpty();

        var lastEvent = battleResult.BattleEvents[battleResult.BattleEvents.Length - 1].ShouldBeOfType<WarriorDied>();
        lastEvent.Survivor.Name.ShouldBe(brutus.Name);
        lastEvent.Survivor.Health.ShouldBeGreaterThan<int>(0);

        lastEvent.DeadMan.Name.ShouldBe(conan.Name);
        lastEvent.DeadMan.Health.ShouldBe<int>(0);
    }

    [Fact]
    public void StartBattle_ShouldThrow_InvalidOperationException()
    {
        var conan = WarriorHelper.CreateBlueSky("Connan", 3, []);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 3);

        var blueSkyStrategy = new BlueSkyBattleStrategy(
            new MagicCardStrategyFactory([]),
            new EchoDecisionSource(0),
            new ActivationRuleEvaluatorSelector([new ChanceActivationRuleEvaluator(new FakeIRandomSource())]),
            new FakeBattleEndEventBuilder());

        Action act = () => blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 1), Time.StartBattleAt);

        var ex = act.ShouldThrow<InvalidOperationException>();
        ex.Message.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public void StartBattle_BrutusShouldDrawn_FightingCard()
    {
        var slot = SlotHelper.Create(new FightingCard(Power.FromValue(1)));
        var conan = WarriorHelper.CreateBlueSky("Connan", 3);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 3, [slot]);

        var blueSkyStrategy = new BlueSkyBattleStrategy(
            new MagicCardStrategyFactory(
                [
                    new FightingCardStrategy()
                ]),
            new EchoDecisionSource(0),
            new ActivationRuleEvaluatorSelector([new ChanceActivationRuleEvaluator(new FakeIRandomSource())]),
            new BattleEndEventBuilder());

        var battleResult = blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 1), Time.StartBattleAt);

        battleResult.BattleEvents.ShouldNotBeEmpty();
        var fightingCardDrawn = battleResult.BattleEvents[3].ShouldBeOfType<CardDrawn>();
        fightingCardDrawn.CardName.ShouldBe(slot.Card.Name);
        fightingCardDrawn.CardHolder.ShouldBe(brutus.Name);
    }

    [Fact]
    public void StartBattle_BrutusDamage_Boosted()
    {
        var slot = SlotHelper.Create(new FightingCard(Power.FromValue(2)));
        var conan = WarriorHelper.CreateBlueSky("Connan", 3);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 3, [slot]);

        var blueSkyStrategy = new BlueSkyBattleStrategy(
            new MagicCardStrategyFactory(
                [
                    new FightingCardStrategy()
                ]),
            new EchoDecisionSource(0),
            new ActivationRuleEvaluatorSelector([new ChanceActivationRuleEvaluator(new FakeIRandomSource())]),
            new BattleEndEventBuilder());

        var maxDamage = brutus.MaxDamage;
        _ = blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 1), Time.StartBattleAt);

        ImmutableArray<DomainEventBase> events = brutus.DequeueDomainEvents();
        events.Length.ShouldBe(1);

        var damageBoosted = events[0].ShouldBeOfType<DamageBoosted>();
        damageBoosted.DamageBeforeBoost.ShouldBe(maxDamage);
        damageBoosted.BoostPower.ShouldBe(((FightingCard)slot.Card).Power.Value);
    }

    [Fact]
    public void StartBattle_BrutusHealed()
    {
        var slot = SlotHelper.Create(new HealingCard(Power.FromValue(2)));
        var fightingCard = SlotHelper.Create(new FightingCard(Power.FromValue(0)));

        var conan = WarriorHelper.CreateBlueSky("Connan", 3, [fightingCard]);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 3, [slot]);

        var blueSkyStrategy = new BlueSkyBattleStrategy(
            new MagicCardStrategyFactory(
                [
                    new HealingCardStrategy(),
                    new FightingCardStrategy()
                ]),
            new EchoDecisionSource(0),
            new ActivationRuleEvaluatorSelector([new ChanceActivationRuleEvaluator(new FakeIRandomSource())]),
            new BattleEndEventBuilder());

        var currentHealth = brutus.Health;
        _ = blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 1), Time.StartBattleAt);

        ImmutableArray<DomainEventBase> events = brutus.DequeueDomainEvents();
        events.Length.ShouldBe(1);

        var evnt = events[0].ShouldBeOfType<WarriorHealed>();
        evnt.OldValue.ShouldBe(currentHealth);
        evnt.NewValue.ShouldBe((int)(currentHealth * ((HealingCard)slot.Card).Power.Value));
    }


    [Fact]
    public void StartBattle_BrutusResurrected()
    {
        var healingCard = SlotHelper.Create(new HealingCard(Power.FromValue(500)));

        var conan = WarriorHelper.CreateBlueSky("Connan", 30);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 1, [healingCard]);

        var blueSkyStrategy = new BlueSkyBattleStrategy(
            new MagicCardStrategyFactory(
                [
                    new HealingCardStrategy(),
                ]),
            new EchoDecisionSource(0),
            new ActivationRuleEvaluatorSelector([new ChanceActivationRuleEvaluator(new FakeIRandomSource())]),
            new BattleEndEventBuilder());

        _ = blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 1), Time.StartBattleAt);

        ImmutableArray<DomainEventBase> events = brutus.DequeueDomainEvents();
        events.Length.ShouldBe(2);

        var evnt = events[0].ShouldBeOfType<Resurrected>();
        evnt.FromHealth.ShouldBe<int>(-650);
        evnt.ToHealth.ShouldBe<int>(0);

        brutus.Health.ShouldBe(500);
    }

    [Fact]
    public void StartBattle_BrutusResurrectedFromZero()
    {
        var healingCard = SlotHelper.Create(new HealingCard(Power.FromValue(200)));

        var conan = WarriorHelper.CreateBlueSky("Connan", 4);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 1, [healingCard]);

        var blueSkyStrategy = new BlueSkyBattleStrategy(
            new MagicCardStrategyFactory(
                [
                    new HealingCardStrategy(),
                ]),
            new EchoDecisionSource(0),
            new ActivationRuleEvaluatorSelector([new ChanceActivationRuleEvaluator(new FakeIRandomSource())]),
            new BattleEndEventBuilder());

        _ = blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 1), Time.StartBattleAt);

        ImmutableArray<DomainEventBase> events = brutus.DequeueDomainEvents();
        events.Length.ShouldBe(2);

        var evnt = events[0].ShouldBeOfType<Resurrected>();
        evnt.FromHealth.ShouldBe<int>(0);
        evnt.ToHealth.ShouldBe<int>(0);

        brutus.Health.ShouldBe(200);
    }

    [Fact]
    public void StartBattle_DequeueDomainEvents_ShouldBeEmpty()
    {
        var healingCard = SlotHelper.Create(new HealingCard(Power.FromValue(2000)));

        var conan = WarriorHelper.CreateBlueSky("Connan", 10);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 1, [healingCard]);

        var blueSkyStrategy = new BlueSkyBattleStrategy(
            new MagicCardStrategyFactory(
                [
                    new HealingCardStrategy(),
                ]),
            new EchoDecisionSource(0),
            new ActivationRuleEvaluatorSelector([new ChanceActivationRuleEvaluator(new FakeIRandomSource())]),
            new BattleEndEventBuilder());

        _ = blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 1), Time.StartBattleAt);

        ImmutableArray<DomainEventBase> firstCall = brutus.DequeueDomainEvents();
        firstCall.Length.ShouldBe(2);

        ImmutableArray<DomainEventBase> secondCall = brutus.DequeueDomainEvents();
        secondCall.Length.ShouldBe(0);
    }

    [Fact]
    public void StartBattle_FirstEventShouldBe_BattleStarted()
    {
        int roundCount = 1;
        var conan = WarriorHelper.CreateBlueSky("Connan", 1);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 1);

        var blueSkyStrategy = CreateBluSkyStrategy();

        var battleResult = blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, roundCount), Time.StartBattleAt);

        battleResult.BattleEvents.ShouldNotBeEmpty();
        var result = battleResult.BattleEvents[0].ShouldBeOfType<BattleStarted>();

        result.StartedAt.ShouldBe(Time.StartBattleAt);

        result.Attacker.ShouldNotBeNull();
        result.Oponent.ShouldNotBeNull();

        result.Attacker.Name.ShouldBe(conan.Name);
        result.Oponent.Name.ShouldBe(brutus.Name);
    }

    [Fact]
    public void StartBattle_BattleResultShouldContains_2Attacks()
    {
        var conan = WarriorHelper.CreateBlueSky("Connan", 1);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 1);

        var blueSkyStrategy = CreateBluSkyStrategy();

        var battleResult = blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 1), Time.StartBattleAt);

        battleResult.BattleEvents.ShouldNotBeEmpty();
        battleResult.BattleEvents.Length.ShouldBe(6);
        var connanAttack = battleResult.BattleEvents[2].ShouldBeOfType<AttackLanded>();
        var brutusAttack = battleResult.BattleEvents[3].ShouldBeOfType<AttackLanded>();

        connanAttack.Damage.ShouldBe(25);
        connanAttack.AttackerName.ShouldBe(conan.Name);
        connanAttack.OponentName.ShouldBe(brutus.Name);

        brutusAttack.Damage.ShouldBe(25);
        brutusAttack.AttackerName.ShouldBe(brutus.Name);
        brutusAttack.OponentName.ShouldBe(conan.Name);
    }

    [Fact]
    public void StartBattle_WhenCtxIsUSedTwiceEachRunShouldHave_5Events()
    {
        var conan = WarriorHelper.CreateBlueSky("Connan", 1);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 1);

        var blueSkyStrategy = CreateBluSkyStrategy();

        var ctx = BattleContext.Create(conan, brutus, 1);
        var battleResult1 = blueSkyStrategy.StartBattle(ctx, Time.StartBattleAt);
        var battleResult2 = blueSkyStrategy.StartBattle(ctx, Time.StartBattleAt);

        battleResult1.BattleEvents.Length.ShouldBe(6);
        battleResult2.BattleEvents.Length.ShouldBe(6);
    }

    [Fact]
    public void StartBattle_SecondEventShouldBe_RoundStarted()
    {
        var conan = WarriorHelper.CreateBlueSky("Connan", 1);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 1);

        var blueSkyStrategy = CreateBluSkyStrategy();

        var battleResult = blueSkyStrategy.StartBattle(BattleContext.Create(conan, brutus, 1), Time.StartBattleAt);

        battleResult.BattleEvents.ShouldNotBeEmpty();
        battleResult.BattleEvents.Length.ShouldBe(6);
        var roundStartedEvent = battleResult.BattleEvents[1].ShouldBeOfType<RoundStarted>();

        roundStartedEvent.Round.ShouldBe(1);
    }

    private static BlueSkyBattleStrategy CreateBluSkyStrategy()
    {
        return new BlueSkyBattleStrategy(
            new MagicCardStrategyFactory(Enumerable.Empty<IMagicCardStrategy>()),
            new EchoDecisionSource(0),
            new ActivationRuleEvaluatorSelector([new ChanceActivationRuleEvaluator(new FakeIRandomSource())]),
            new BattleEndEventBuilder());
    }
}
