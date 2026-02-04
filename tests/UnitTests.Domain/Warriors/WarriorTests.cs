using System;
using System.Collections.Generic;
using System.Text;
using Domain.Battles.Spheres;
using Domain.MagicCards;
using Domain.Shared;
using Domain.Warriors;
using Domain.Warriors.Rules;
using Shouldly;

namespace Domain.UnitTests.Warriors;

public class WarriorTests
{
    [Fact]
    public void CreateWarrior_ShouldCreate_Conan()
    {
        WarriorId warriorId = WarriorId.New();
        string name = "Barbar Conan";
        SphereBase sphere = SphereBase.BlueSky;
        Level level = Level.FromNumber(1);
        IEnumerable<MagicCardBase> cards = Enumerable.Empty<MagicCardBase>();

        Warrior barbarConan = Warrior.Create(warriorId, name, sphere, level, cards);

        barbarConan.Id.ShouldBe(warriorId);
        barbarConan.CurrentSphere.ShouldBe(sphere);
        barbarConan.Level.ShouldBe(level);
        barbarConan.Name.ShouldBe(name);
    }

    [Fact]
    public void CreateWarrior_ShouldHaveHealth_200()
    {
        var conan = WarriorHelper.CreateBlueSky("Conan", 2);
        conan.Health.ShouldBe(200);
    }

    [Fact]
    public void CreateWarrior_ShouldHaveMaxDamage_50()
    {        
        var conan = WarriorHelper.CreateBlueSky("Conan", 2);        
        conan.MaxDamage.ShouldBe(50);
    }

    [Fact]
    public void CreateWarrior_WhenNameLengthIs1_ShouldViolatesWarrirNameLengthRule()
    {        
        Action act = () => WarriorHelper.CreateBlueSky("C");
        var ex = act.ShouldThrow<BusinessRuleValidationException>();
        ex.ToString().ShouldNotBeNullOrEmpty();
        var rule = ex.BrokenRule.ShouldBeOfType<WarrirNameLengthRule>();
        rule.Message.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public void CreateWarrior_WhenNameLengthIs0_ShouldViolatesWarrirNameLengthRule()
    {
        Action act = () => WarriorHelper.CreateBlueSky("");
        var ex = act.ShouldThrow<BusinessRuleValidationException>();
        ex.ToString().ShouldNotBeNullOrEmpty();
        var rule = ex.BrokenRule.ShouldBeOfType<WarrirNameLengthRule>();
        rule.Message.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public void CreateWarrior_WhenNameLengthGreaterThen50_ShouldViolatesWarrirNameLengthRule()
    {
        Action act = () => WarriorHelper.CreateBlueSky("C3333333333333333333333333333333333333333333333333333333333333");
        var ex = act.ShouldThrow<BusinessRuleValidationException>();
        ex.ToString().ShouldNotBeNullOrEmpty();
        var rule = ex.BrokenRule.ShouldBeOfType<WarrirNameLengthRule>();
        rule.Message.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public void CreateWarrior_WhenLengthIs50_WarrirIsCreated()
    {
        var w = WarriorHelper.CreateBlueSky("12345678901234567890123456789012345678901234567890");
        w.Name.Length.ShouldBe(50);
    }

    [Fact]
    public void CreateWarrior_WhenLengthIs2_WarrirIsCreated()
    {
        var w = WarriorHelper.CreateBlueSky("12");
        w.Name.Length.ShouldBe(2);
    }

    [Fact]
    public void CreateWarrior_WhenLengthIs3_WarrirIsCreated()
    {
        var w = WarriorHelper.CreateBlueSky("123");
        w.Name.Length.ShouldBe(3);
    }
}
