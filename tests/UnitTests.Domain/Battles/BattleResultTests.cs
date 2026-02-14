using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Domain.Battles;
using Domain.Battles.Events;
using Domain.Battles.Rules;
using Domain.Battles.Spheres;
using Domain.Shared;
using Shouldly;

namespace Domain.UnitTests.Battles;

public sealed  class BattleResultTests
{
    [Fact]
    public void BattleResult_All_ShouldNotBeEmpty()
    {
        Action act = () => BattleResult.Create(ImmutableArray.CreateRange<IBattleEvent>([]));

        var ex = act.ShouldThrow<BusinessRuleValidationException>();
        ex.Message.ShouldNotBeNullOrEmpty();
        var rule = ex.BrokenRule.ShouldBeOfType<BattleEventsCannotBeEmptyRule>();
        rule.Message.ShouldNotBeNullOrEmpty();
    }

}
