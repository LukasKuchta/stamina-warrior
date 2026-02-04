using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Domain.MagicCards;
using Domain.MagicCards.Rules;
using Domain.Shared;
using Shouldly;

namespace Domain.UnitTests.MagicCards;

public sealed class PowerTests
{
    [Fact]
    public void FromValue_ShouldViolates_MagicPowerCanBePositiveRule()
    {
        Action act = () => Power.FromValue(-1);
        var ex = act.ShouldThrow<BusinessRuleValidationException>();
        var rule = ex.BrokenRule.ShouldBeOfType<MagicPowerCanBePositiveRule>();
        rule.Message.ShouldNotBeNullOrEmpty();
    }
}
