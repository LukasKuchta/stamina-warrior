using Domain.ActivationRules;
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
        var card = new HealingCard(Power.FromValue(5));
        card.Name.ShouldNotBeNullOrEmpty();
        card.Name.ShouldBe("Card of healing");        
        card.Power.Value.ShouldBe(5);
    }

    [Fact]
    public void HealingCard_ShouldViolate_ZeroPowerWillKillYouRule()
    {
        Action act = () => HealingCard.Create(Power.Zero);
        var ex = act.ShouldThrow<BusinessRuleValidationException>();
        var rule = ex.BrokenRule.ShouldBeOfType<ZeroPowerWiollKillYouRule>();
        rule.Message.ShouldNotBeNullOrEmpty();
    }


    [Fact]
    public void FightingCard_ShouldHave_ValidValues()
    {
        var card = new FightingCard(Power.FromValue(2));
        card.Name.ShouldNotBeNullOrEmpty();
        card.Name.ShouldBe("Fighting card");        
        card.Power.Value.ShouldBe(2);
    }

    [Fact]
    public void StealingCard_ShouldHave_ValidValues()
    {
        var card = new StealingCard();
        card.Name.ShouldNotBeNullOrEmpty();
        card.Name.ShouldBe("Stealing card");        
    }

    [Fact]
    public void ThornCard_ShouldHave_ValidValues()
    {
        var card = new ThornDamageCard(Power.FromValue(20));
        card.Name.ShouldNotBeNullOrEmpty();
        card.Name.ShouldBe("Card of thorn's damage");        
        card.Power.Value.ShouldBe(20);
    }

    [Fact]
    public void CoursedCard_ShouldHave_ValidValues()
    {
        var card = new CoursedCard(Power.FromValue(2000));
        card.Name.ShouldNotBeNullOrEmpty();
        card.Name.ShouldBe("Coursed card");       
        card.Power.Value.ShouldBe(2000);
    }
}
