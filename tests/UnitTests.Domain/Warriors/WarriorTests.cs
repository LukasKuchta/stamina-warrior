using System;
using System.Collections.Generic;
using System.Text;
using Domain.MagicCards;
using Domain.Warriors;
using Shouldly;

namespace Domain.UnitTests.Warriors;

public class WarriorTests
{
    [Fact]
    public void CreateWarrior_IsSuccessful()
    {
        WarriorId warriorId = WarriorId.New();
        string name = "Barbar Conan";
        Sphere sphere = Sphere.BlueSky;
        Level level = Level.FromNumber(1);
        IEnumerable<MagicCardBase> cards = Enumerable.Empty<MagicCardBase>();

        Warrior barbarConan = Warrior.Create(warriorId, name, sphere, level, cards);

        barbarConan.Id.ShouldBe(warriorId);
        barbarConan.CurrentSphere.ShouldBe(sphere);
        barbarConan.Level.ShouldBe(level);        
        barbarConan.Name.ShouldBe(name);        
    }
}
