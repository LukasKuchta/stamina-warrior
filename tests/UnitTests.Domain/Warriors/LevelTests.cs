using System;
using System.Collections.Generic;
using System.Text;
using Domain.Battles;
using Domain.Shared;
using Domain.Warriors;
using Domain.Warriors.Rules;
using Shouldly;

namespace Domain.UnitTests.Warriors;

public class LevelTests
{
    [Fact]
    public void FromNumbe_WhenNegativeLevel_ShouldViolateLevelCannotBeNegativeRule()
    {
        Action act = () => Level.FromNumber(-100);

        var ex = act.ShouldThrow<BusinessRuleValidationException>();
        ex.Message.ShouldNotBeNullOrEmpty();
        ex.BrokenRule.ShouldBeOfType<LevelCannotBeNegativeRule>();
    }

    [Fact]
    public void FromNumbe_WhenZeroLevel_ShouldViolateLevelCannotBeNegativeRule()
    {
        Action act = () => Level.FromNumber(0);

        var ex = act.ShouldThrow<BusinessRuleValidationException>();
        ex.Message.ShouldNotBeNullOrEmpty();
        ex.BrokenRule.ShouldBeOfType<LevelCannotBeNegativeRule>();
    }
}
