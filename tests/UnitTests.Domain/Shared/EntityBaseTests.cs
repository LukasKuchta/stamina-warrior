using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Domain.Shared;
using Domain.Warriors;
using Shouldly;

namespace Domain.UnitTests.Shared;

public class EntityBaseTests
{
    [Fact]
    public void DequeueDomainEvents_ShouldHave_ZeroEvents()
    {
        var conan = WarriorHelper.CreateBlueSky("Conan");
        var events = conan.DequeueDomainEvents();
        events.Length.ShouldBe(0);
        events.ShouldBe(ImmutableArray<DomainEventBase>.Empty);
    }
}
