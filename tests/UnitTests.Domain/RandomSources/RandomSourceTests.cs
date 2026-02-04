using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Domain.MagicCards;
using Domain.RandomSources;
using Domain.Shared;
using Shouldly;

namespace Domain.UnitTests.RandomSources;

public sealed class RandomSourceTests
{
    [Fact]
    public void Succeeds_WhenChanceAlways_ShouldReturnTrue()
    {
        var source = new RandomSource();
        source.Succeeds(Chance.Always).ShouldBeTrue();
    }

    [Fact]
    public void Succeeds_WhenChanceAlways_ShouldReturnNotBeFalse()
    {
        var source = new RandomSource();
        source.Succeeds(Chance.Always).ShouldNotBe(false);
    }

    [Fact]
    public void Succeeds_WhenChanceNever_ShouldReturnFalse()
    {
        var source = new RandomSource();
        source.Succeeds(Chance.Never).ShouldBeFalse();
    }

    [Fact]
    public void Succeeds_WhenChanceNever_ShouldReturnNotBeTrue()
    {
        var source = new RandomSource();
        source.Succeeds(Chance.Never).ShouldNotBe(true);
    }

    [Fact]
    public void NextIntInclusive_SholdThrow_ArgumentException()
    {
        var source = new RandomSource();
        Action act = () => source.NextIntInclusive(-1);
        var ex = act.ShouldThrow<ArgumentException>();
        ex.ParamName.ShouldBe("maxInclusive");
        ex.Message.ShouldStartWith("maxInclusive must be greater or equal zero.");
    }

    [Fact]
    public void NextIntInclusive_SholdNotThrow_ArgumentException()
    {
        var source = new RandomSource();
        Action act = () => source.NextIntInclusive(0);
        act.ShouldNotThrow();
    }

    [Fact]
    public void Succeeds_WhenRandomEqualsChanceValue_ShouldBeFalse()
    {
        var sut = new RandomSource(() => 0.5);
        sut.Succeeds(Chance.CoinFlip).ShouldBeFalse();
    }


    [Fact]
    public void Succeeds_WhenRandomEqualsChanceValue_ShouldBeTrue()
    {
        var sut = new RandomSource(() => 0.7);
        sut.Succeeds(Chance.CoinFlip).ShouldBeFalse();
    }

    [Fact]
    public void Succeeds_WithCoinFlip_ShouldReturn_BothTrueAndFalse()
    {
        var sut = new RandomSource();

        var results = Enumerable.Range(0, 1000)
            .Select(_ => sut.Succeeds(Chance.CoinFlip))
            .ToArray();

        results.Any(x => x).ShouldBeTrue();
        results.Any(x => !x).ShouldBeTrue();
    }

    [Fact]
    public void NextIntInclusive_ShouldProduceMultipleDifferentValues()
    {
        var sut = new RandomSource();

        var set = new HashSet<int>();
        for (var i = 0; i < 200; i++)
        {
            set.Add(sut.NextIntInclusive(10));
        }            

        set.Count.ShouldBeGreaterThan(1);
    }

    [Fact]
    public void NextIntInclusive_ShouldNeverReturnNegative()
    {
        var sut = new RandomSource();

        for (var i = 0; i < 200; i++)
        {
            var x = sut.NextIntInclusive(10);
            x.ShouldBeGreaterThanOrEqualTo(0);
        }
    }
}
