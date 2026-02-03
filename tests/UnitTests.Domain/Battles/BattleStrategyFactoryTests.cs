using System;
using System.Collections.Generic;
using System.Text;
using Domain.Battles;
using Domain.Battles.Events;
using Domain.Battles.Spheres;
using Domain.Battles.Strategies;
using Domain.MagicCards;
using Domain.UnitTests.Battles.Shared;
using Shouldly;

namespace Domain.UnitTests.Battles;

public class BattleStrategyFactoryTests
{
    [Fact]
    public void SelectBy_WhenSelectByBlueSky_ShouldReturnBattleStrategyHelper()
    {
        var factory = new BattleStrategyFactory([BattleStrategyHelper.CreateDefaultBlueSky()]);

        var strategy = factory.SelectBy(SphereBase.BlueSky);

        strategy.ShouldBeAssignableTo<IBattleStrategy<BlueSkysphere>>();
    }

    [Fact]
    public void SelectBy_WhenStrategyIsNotRegistered_ShouldThrowEception()
    {
        var factory = new BattleStrategyFactory([]);

        Action act = () => factory.SelectBy(SphereBase.BlueSky);

        var ex = act.ShouldThrow<ArgumentException>();
        ex.Message.ShouldNotBeNullOrEmpty();
    }
}
