using Domain.ActivationRules;
using Domain.BattlePlans;
using Domain.Battles;
using Domain.RandomSources;
using Domain.UnitTests.Battles.Shared;
using Domain.Warriors;
using Shouldly;

namespace Domain.UnitTests.Battles;

public sealed class FightDecisionSourceTests
{
    [Fact]
    public void PickBaseDamage_WhenMaxDamageIs100_ShouldBe100()
    {
        var decissionSource = new FightDecisionSource(new FakeIRandomSource(100));
        decissionSource.PickDamage(100).ShouldBe(100);
    }

    [Fact]
    public void PickCardIndex_WhenMaxIndexIs2_ShouldBe2()
    {
        var decissionSource = new FightDecisionSource(new FakeIRandomSource(2));
        decissionSource.PickSlotIndex(2).ShouldBe(2);
    }

    [Fact]
    public void HitCheck_ShouldReturnFalse()
    {
        var conan = WarriorHelper.CreateBlueSky("Connan", 1, []);

        var decissionSource = new FightDecisionSource(new FakeIRandomSource(0, false));
        decissionSource.HitCheck(conan).ShouldBeFalse();
    }

    [Fact]
    public void HitCheck_ShouldReturnTrue()
    {
        var conan = WarriorHelper.CreateBlueSky("Connan", 1, []);

        var decissionSource = new FightDecisionSource(new FakeIRandomSource(0));
        decissionSource.HitCheck(conan).ShouldBeTrue();
    }

    [Fact]
    public void HitCheck_WhenChanceIsBelowRoll_ShouldBeFalse()
    {
        var random = new DeterministicRandomSource(0.80f);
        var sut = new FightDecisionSource(random);

        var conan = WarriorHelper.CreateBlueSky("Connan", 1, []);
        conan.ChangeAccuracy(1);
        conan.ChangeEvasion(0);     

        sut.HitCheck(conan).ShouldBeFalse();
    }

    [Fact]
    public void HitCheck_WhenChanceIsAboveRoll_ShouldBeTrue()
    {
        var random = new DeterministicRandomSource(0.70f);
        var sut = new FightDecisionSource(random);

        var conan = WarriorHelper.CreateBlueSky("Connan", 1, []);
        conan.ChangeAccuracy(30);
        conan.ChangeEvasion(20);

        sut.HitCheck(conan).ShouldBeTrue();
    }

    private sealed class DeterministicRandomSource : IRandomSource
    {
        private readonly float _roll;
        public DeterministicRandomSource(float roll) => _roll = roll;

        public int NextIntInclusive(int maxInclusive) => 0;

        public bool Succeeds(Chance chance) => _roll < chance.Value;
    }
}
