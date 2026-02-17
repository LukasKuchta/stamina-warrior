using Domain.ActivationRules;
using Domain.Battles;
using Domain.UnitTests.Battles.Shared;
using Shouldly;

namespace Domain.UnitTests.Battles;

public sealed class FightDecisionSourceTests
{
    [Fact]
    public void PickBaseDamage_WhenMaxDamageIs100_ShouldBe100()
    {
        var decissionSource = new FightDecisionSource(new FakeIRandomSource(100));
        decissionSource.PickBaseDamage(100).ShouldBe(100);
    }

    [Fact]
    public void PickCardIndex_WhenMaxIndexIs2_ShouldBe2()
    {
        var decissionSource = new FightDecisionSource(new FakeIRandomSource(2));
        decissionSource.PickSlotIndex(2).ShouldBe(2);
    }
}
