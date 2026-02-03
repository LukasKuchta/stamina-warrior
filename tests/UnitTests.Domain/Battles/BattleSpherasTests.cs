using System;
using System.Collections.Generic;
using System.Text;
using Domain.Battles;
using Domain.Battles.Events;
using Domain.Battles.Spheres;
using Shouldly;

namespace Domain.UnitTests.Battles;

public class BattleSpherasTests
{
    [Fact]
    public void SphereBase_All_ShouldNotBeEmpty()
    {
        SphereBase.All.ShouldNotBeEmpty();

        foreach (var sphere in SphereBase.All)
        {
            sphere.Name.ShouldNotBeNullOrEmpty();
        }
    }

    [Fact]
    public void BlueSky_ShouldHaveName_BlueSky()
    {
        SphereBase.BlueSky.Name.ShouldBe("BlueSky");
    }

    [Fact]
    public void Betweenworld_ShouldHaveName_Betweenworld()
    {
        SphereBase.Betweenworld.Name.ShouldBe("Betweenworld");
    }

    [Fact]
    public void Deepvault_ShouldHaveName_Deepvault()
    {
        SphereBase.Deepvault.Name.ShouldBe("Deepvault");
    }
}
