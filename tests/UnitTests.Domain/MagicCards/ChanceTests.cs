using Domain.MagicCards;
using Domain.MagicCards.Rules;
using Domain.Shared;
using Shouldly;

namespace Domain.UnitTests.MagicCards;

public sealed class ChanceTests
{
    [Fact]
    public void FromValue_ShouldViolates_ValueCanBeBetweenZeoroAndOneRule()
    {
        Action act = () => Chance.FromValue(-1);
        var ex = act.ShouldThrow<BusinessRuleValidationException>();
        var rule = ex.BrokenRule.ShouldBeOfType<ValueCanBeBetweenZeoroAndOneRule>();
        rule.Message.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public void FromValue_WhenValueIs0_ChanceMustExist()
    {
        var chance = Chance.FromValue(0);
        chance.Value.ShouldBe(0);
    }

    [Fact]
    public void FromValue_WhenValueIs1_ChanceMustExist()
    {
        var chance = Chance.FromValue(1);
        chance.Value.ShouldBe(1);
    }

    [Fact]
    public void FromValue_WhenValueIs0_5_ChanceMustExist()
    {
        var chance = Chance.FromValue(0.5f);
        chance.Value.ShouldBe(0.5f);
    }
}
