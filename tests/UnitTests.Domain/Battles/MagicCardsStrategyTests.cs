//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Domain.Battles;
//using Domain.Battles.Strategies;
//using Domain.MagicCards;
//using Domain.MagicCards.Cards;
//using Domain.MagicCards.Strategies;
//using Domain.RandomSources;
//using Domain.Shared;
//using Domain.Warriors.Events;
//using NSubstitute;
//using Shouldly;

//namespace Domain.UnitTests.Battles;

//public class MagicCardsStrategyTests
//{
//    private readonly IEnumerable<MagicCardBase> EmptyDeckOfCards = Enumerable.Empty<MagicCardBase>();

//    [Fact]
//    public void FightingCard_ConanShouldHit_75Damage()
//    {
//        var cards = new List<MagicCardBase> { FightingCard.CreateOneHundredPercentChance(3) };

//        var conan = WarriorHelper.CreateBlueSkyWarrior("Connan", 1, cards);
//        var brutus = WarriorHelper.CreateBlueSkyWarrior("Brutus", 1, EmptyDeckOfCards);

//        var strategies = new List<IMagicCardStrategy>
//        {
//            new FightingCardStrategy()
//        };

//        var blueSkyStrategy = CreateBluSkyStrategy(strategies);

//        var battleResultBase = blueSkyStrategy.StartBattle(new BattleContext(conan, brutus, 1));

//        var battleResult = battleResultBase.ShouldBeAssignableTo<BattleResult>();

//        battleResult.BattleEvents.Winner.ShouldBe(conan);
//        battleResult.Loser.ShouldBe(brutus);

//        conan.Health.ShouldBeEquivalentTo(75);
//        brutus.Health.ShouldBeEquivalentTo(25);
//    }

//    [Fact]
//    public void HealingCard_ShouldHealTo_175()
//    {
//        var cards = new List<MagicCardBase> { HealingCard.Create(2) };

//        var conan = WarriorHelper.CreateBlueSkyWarrior("Connan", 1, cards);
//        var brutus = WarriorHelper.CreateBlueSkyWarrior("Brutus", 1, EmptyDeckOfCards);

//        var strategies = new List<IMagicCardStrategy>
//        {
//            new HealingCardStrategy()
//        };

//        var blueSkyStrategy = CreateBluSkyStrategy(strategies);

//        var battleResultBase = blueSkyStrategy.StartBattle(new BattleContext(conan, brutus, 1));

//        var battleResult = battleResultBase.ShouldBeAssignableTo<BattleResult>();

//        battleResult.Winner.ShouldBe(conan);
//        battleResult.Loser.ShouldBe(brutus);

//        conan.Health.ShouldBeEquivalentTo(175);
//        brutus.Health.ShouldBeEquivalentTo(75);
//    }

//    [Fact]
//    public void SelfDamageCard_ConanShould_Die()
//    {
//        var cards = new List<MagicCardBase> { SelfDamageCard.Create(3) };

//        var conan = WarriorHelper.CreateBlueSkyWarrior("Connan", 1, cards);
//        var brutus = WarriorHelper.CreateBlueSkyWarrior("Brutus", 1, EmptyDeckOfCards);

//        var strategies = new List<IMagicCardStrategy>
//        {
//            new SelfDamageStrategy()
//        };

//        var blueSkyStrategy = CreateBluSkyStrategy(strategies);

//        var battleResultBase = blueSkyStrategy.StartBattle(new BattleContext(conan, brutus, 1));

//        var battleResult = battleResultBase.ShouldBeAssignableTo<BattleResult>();

//        battleResult.Winner.ShouldBe(brutus);
//        battleResult.Loser.ShouldBe(conan);

//        conan.Health.ShouldBeEquivalentTo(0);
//        brutus.Health.ShouldBeEquivalentTo(75);
//    }

//    [Fact]
//    public void StealingCard_Test()
//    {
//        var selfDamageCard = SelfDamageCard.Create(2);
//        var connansCard = new List<MagicCardBase> { StealingCard.CreateOneHUndretdPercentChance() };
//        var brutusCards = new List<MagicCardBase> { selfDamageCard };

//        var conan = WarriorHelper.CreateBlueSkyWarrior("Connan", 1, connansCard);
//        var brutus = WarriorHelper.CreateBlueSkyWarrior("Brutus", 1, brutusCards);

//        var randomSource = Substitute.For<IRandomSource>();
//        randomSource.NextIntInclusive(Arg.Any<int>()).Returns(0);

//        var strategies = new List<IMagicCardStrategy>
//        {
//            new StealingCardStrategy(randomSource),
//            new SelfDamageStrategy()
//        };

//        var blueSkyStrategy = CreateBluSkyStrategy(strategies);

//        var battleResultBase = blueSkyStrategy.StartBattle(new BattleContext(conan, brutus, 2));

//        var battleResult = battleResultBase.ShouldBeAssignableTo<BattleResult>();

//        battleResult.Winner.ShouldBe(brutus);
//        battleResult.Loser.ShouldBe(conan);

//        conan.Health.ShouldBeEquivalentTo(0);
//        brutus.Health.ShouldBeEquivalentTo(50);

//        var @evet = conan.DomainEvents.AsEnumerable<DomainEventBase>().FirstOrDefault();

//        @evet.ShouldNotBeNull();
//        var cardStolen = @evet.ShouldBeAssignableTo<CardStolen>();
//        cardStolen.CardName.ShouldBe(selfDamageCard.Name);
//    }

//    private static BlueSkyBattleStrategy CreateBluSkyStrategy(List<IMagicCardStrategy> strategies)
//    {
//        return new BlueSkyBattleStrategy(
//            new MagicCardStrategyFactory(strategies),
//            new EchoDecisionSource(0));
//    }
//}
