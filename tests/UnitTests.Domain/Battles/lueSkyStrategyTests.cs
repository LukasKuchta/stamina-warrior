using System;
using System.Collections.Generic;
using System.Text;
using Domain.Battles;
using Domain.Battles.Events;
using Domain.Battles.Strategies;
using Domain.MagicCards;
using Domain.MagicCards.Cards;
using Domain.MagicCards.Strategies;
using Domain.Warriors;
using NSubstitute;
using Shouldly;

namespace Domain.UnitTests.Battles;

public class BlueSkyStrategBattleResultsyTests
{
    [Fact]
    public void Battle_ShouldWin_Connan()
    {
        var conan = WarriorHelper.CreateBlueSkyWarrior("Connan", 3);
        var brutus = WarriorHelper.CreateBlueSkyWarrior("Brutus", 1);

        var blueSkyStrategy = CreateBluSkyStrategy();

        var battleResultBase = blueSkyStrategy.StartBattle(new BattleContext(conan, brutus, 1));

        var battleResult = battleResultBase.ShouldBeAssignableTo<BattleResult>();

        battleResult.BattleEvents.ShouldNotBeEmpty();
        var lastEvent = battleResult.BattleEvents[battleResult.BattleEvents.Length - 1].ShouldBeOfType<BattleFinished>();

        lastEvent.Winner.Name.ShouldBe(conan.Name);
        lastEvent.Looser.Name.ShouldBe(brutus.Name);
    }

    [Fact]
    public void Battle_ShouldEnds_Tie()
    {
        var conan = WarriorHelper.CreateBlueSkyWarrior("Connan", 1);
        var brutus = WarriorHelper.CreateBlueSkyWarrior("Brutus", 1);

        var blueSkyStrategy = CreateBluSkyStrategy();

        var battleResult = blueSkyStrategy.StartBattle(new BattleContext(conan, brutus, 1));

        battleResult.BattleEvents.ShouldNotBeEmpty();        
        battleResult.BattleEvents[battleResult.BattleEvents.Length - 1].ShouldBeOfType<BattleFinishedTied>();
    }

    [Fact]
    public void Battle_ShouldEnds_DoubleKo()
    {
        var conan = WarriorHelper.CreateBlueSkyWarrior("Connan", 1);
        var brutus = WarriorHelper.CreateBlueSkyWarrior("Brutus", 1);

        var blueSkyStrategy = CreateBluSkyStrategy();


        var battleResult = blueSkyStrategy.StartBattle(new BattleContext(conan, brutus, 10));

        battleResult.BattleEvents.ShouldNotBeEmpty();        
        battleResult.BattleEvents[battleResult.BattleEvents.Length - 1].ShouldBeOfType<DoubleKnockoutOccurred>();
    }

    private static BlueSkyBattleStrategy CreateBluSkyStrategy()
    {
        return new BlueSkyBattleStrategy(
            new MagicCardStrategyFactory(Enumerable.Empty<IMagicCardStrategy>()),
            new EchoDecisionSource(0));
    }


}
