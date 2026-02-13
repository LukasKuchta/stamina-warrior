using Domain.Battles;
using Domain.Battles.Rules;
using Domain.Shared;
using Shouldly;

namespace Domain.UnitTests.Battles;

public class BattleContextTests
{
    [Fact]
    public void CreateBattleContext_WithZeroRounds_ShouldViolateRoundCannotBeLowerOrEqualZeroRule()
    {
        var conan = WarriorHelper.CreateBlueSky("Conan");
        var brutus = WarriorHelper.CreateBlueSky("Brutus");

        Action act = () => BattleContext.Create(conan, brutus, 0);

        var ex = act.ShouldThrow<BusinessRuleValidationException>();
        ex.Message.ShouldNotBeNullOrEmpty();
        ex.BrokenRule.ShouldBeOfType<RoundCannotBeLowerOrEqualZeroRule>();
    }

    [Fact]
    public void CreateBattleContext_WithNegativeRounds_ShouldViolateRoundCannotBeLowerOrEqualZeroRule()
    {
        var conan = WarriorHelper.CreateBlueSky("Conan");
        var brutus = WarriorHelper.CreateBlueSky("Brutus");

        Action act = () => BattleContext.Create(conan, brutus, -1);

        var ex = act.ShouldThrow<BusinessRuleValidationException>();
        ex.Message.ShouldNotBeNullOrEmpty();
        ex.BrokenRule.ShouldBeOfType<RoundCannotBeLowerOrEqualZeroRule>();
    }

    [Fact]
    public void CreateBattleContext_WhenWarriorsHaveDifferentSphere_ShouldViolateAttackerAndOpponentSpehereCheckRule()
    {
        var conan = WarriorHelper.CreateBlueSky("Conan");
        var brutus = WarriorHelper.CreateBetweenworld("Brutus");

        Action act = () => BattleContext.Create(conan, brutus, 1);

        var ex = act.ShouldThrow<BusinessRuleValidationException>();
        ex.Message.ShouldNotBeNullOrEmpty();
        ex.BrokenRule.ShouldBeOfType<AttackerAndOpponentSpehereCheckRule>();
    }

    [Fact]
    public void CreateBattleContext_WhenAttackerAndOpponetAreTheSame_ShouldViolateAttackerAndOpponentCannotBeTheSameRule()
    {
        var conan = WarriorHelper.CreateBlueSky("Conan");

        Action act = () => BattleContext.Create(conan, conan, 1);

        var ex = act.ShouldThrow<BusinessRuleValidationException>();
        ex.Message.ShouldNotBeNullOrEmpty();
        ex.BrokenRule.ShouldBeOfType<AttackerAndOpponentCannotBeTheSameRule>();
    }
}
