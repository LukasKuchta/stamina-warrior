using Domain.Battles.Rules;
using Domain.MagicCards;
using Domain.MagicCards.Cards;
using Domain.Shared;
using Shouldly;

namespace Domain.UnitTests.MagicCards;

public sealed class MagicCardBaseTests
{
    [Fact]
    public void HealingCard_ShouldHave_ValidValues()
    {
        var card = new HealingCard(Chance.CoinFlip, Power.FromValue(5));
        card.Name.ShouldNotBeNullOrEmpty();
        card.Name.ShouldBe("Card of healing");
        card.ActivationChance.Value.ShouldBe(Chance.CoinFlip.Value);
        card.ActivationChance.ShouldBe(Chance.CoinFlip);
        card.Power.Value.ShouldBe(5);
    }

    [Fact]
    public void HealingCard_ShouldViolate_ZeroPowerWillKillYouRule()
    {
        Action act = () => HealingCard.Create(Chance.CoinFlip, Power.Zero);
        var ex = act.ShouldThrow<BusinessRuleValidationException>();
        var rule = ex.BrokenRule.ShouldBeOfType<ZeroPowerWiollKillYouRule>();
        rule.Message.ShouldNotBeNullOrEmpty();
    }


    [Fact]
    public void FightingCard_ShouldHave_ValidValues()
    {
        var card = new FightingCard(Chance.CoinFlip, Power.FromValue(2));
        card.Name.ShouldNotBeNullOrEmpty();
        card.Name.ShouldBe("Fighting card");
        card.ActivationChance.Value.ShouldBe(Chance.CoinFlip.Value);
        card.ActivationChance.ShouldBe(Chance.CoinFlip);
        card.Power.Value.ShouldBe(2);
    }

    [Fact]
    public void StealingCard_ShouldHave_ValidValues()
    {
        var card = new StealingCard(Chance.Always);
        card.Name.ShouldNotBeNullOrEmpty();
        card.Name.ShouldBe("Stealing card");
        card.ActivationChance.Value.ShouldBe(Chance.Always.Value);
        card.ActivationChance.ShouldBe(Chance.Always);
    }

    [Fact]
    public void ThornCard_ShouldHave_ValidValues()
    {
        var card = new ThornDamageCard(Chance.Never, Power.FromValue(20));
        card.Name.ShouldNotBeNullOrEmpty();
        card.Name.ShouldBe("Card of thorn's damage");
        card.ActivationChance.Value.ShouldBe(Chance.Never.Value);
        card.ActivationChance.ShouldBe(Chance.Never);
        card.Power.Value.ShouldBe(20);
    }

    [Fact]
    public void CoursedCard_ShouldHave_ValidValues()
    {
        var card = new CoursedCard(Chance.FromValue(0.3f), Power.FromValue(2000));
        card.Name.ShouldNotBeNullOrEmpty();
        card.Name.ShouldBe("Coursed card");
        card.ActivationChance.Value.ShouldBe(0.3f);
        card.Power.Value.ShouldBe(2000);
    }
}
