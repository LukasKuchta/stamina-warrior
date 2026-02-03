using System;
using System.Collections.Generic;
using System.Text;
using Domain.Battles.Spheres;
using Domain.MagicCards;
using Domain.Warriors;
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
}
