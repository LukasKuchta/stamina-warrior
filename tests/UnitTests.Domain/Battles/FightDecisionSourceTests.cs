using System;
using System.Collections.Generic;
using System.Text;
using Domain.Battles;
using Domain.Battles.Spheres;
using Domain.MagicCards;
using Domain.UnitTests.Battles.Shared;
using Shouldly;

namespace Domain.UnitTests.Battles;

public sealed class FightDecisionSourceTests
{
    [Fact]
    public void TryActivate_WhenChanceIsAlways_ShouldBeTrue()
    {
        var decissionSource = new FightDecisionSource(new FakeIRandomSource());
        decissionSource.TryActivate(Chance.Always).ShouldBe(true);
    }

    [Fact]
    public void PickBaseDamage_WhenMaxDamageIs100_ShouldBe100()
    {
        var decissionSource = new FightDecisionSource(new FakeIRandomSource());
        decissionSource.PickBaseDamage(100).ShouldBe(100);
    }

    [Fact]
    public void PickCardIndex_WhenMaxIndexIs2_ShouldBe2()
    {
        var decissionSource = new FightDecisionSource(new FakeIRandomSource());
        decissionSource.PickCardIndex(2).ShouldBe(2);
    }
}
