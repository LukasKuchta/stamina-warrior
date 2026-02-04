using System;
using System.Collections.Generic;
using System.Text;
using Domain.Battles;
using Domain.Battles.Spheres;
using Domain.Battles.Strategies;
using Domain.MagicCards;
using Domain.MagicCards.Cards;
using Domain.MagicCards.Strategies;
using Domain.RandomSources;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Domain.UnitTests;

public sealed class DependencyInjectionTests
{
    [Fact]
    public void DomainServices_DI_graph_is_valid()
    {
        var services = new ServiceCollection();
        services.AddDomainServices();

        Should.NotThrow(() =>
        {
            using var sp = services.BuildServiceProvider(new ServiceProviderOptions
            {
                ValidateOnBuild = true,
                ValidateScopes = true
            });
        });
    }

    public static IEnumerable<object[]> KeySingletons =>
[
    [typeof(IBattleStrategy<BlueSkysphere>), typeof(BlueSkyBattleStrategy)],
    [typeof(IBattleStrategyFactory), typeof(BattleStrategyFactory)],
    [typeof(IBattleEndEventBuilder), typeof(BattleEndEventBuilder)],
    [typeof(IMagicCardStrategy<HealingCard>), typeof(HealingCardStrategy)],
    [typeof(IMagicCardStrategy<FightingCard>), typeof(FightingCardStrategy)],
    [typeof(IMagicCardStrategy<StealingCard>), typeof(StealingCardStrategy)],
    [typeof(IMagicCardStrategy<ThornDamageCard>), typeof(ThornDamageStrategy)],
    [typeof(IMagicCardStrategy<CoursedCard>), typeof(CoursedCardStrategy)],
    [typeof(IRandomSource), typeof(RandomSource)],
    [typeof(IFightDecisionSource), typeof(FightDecisionSource)],
    [typeof(IMagicCardStrategyFactory), typeof(MagicCardStrategyFactory)]
];

    [Theory]
    [MemberData(nameof(KeySingletons))]
    public void Registers_singletons(Type service, Type impl)
    {
        var services = new ServiceCollection();
        services.AddDomainServices();

        services.ShouldContain(d =>
            d.ServiceType == service &&
            d.ImplementationType == impl &&
            d.Lifetime == ServiceLifetime.Singleton);
    }

    [Fact]
    public void AddDomainServices_registers_expected_magic_card_strategy_types()
    {
        var services = new ServiceCollection();
        services.AddDomainServices();
        using var sp = services.BuildServiceProvider();

        var types = sp.GetServices<IMagicCardStrategy>()
            .Select(s => s.CardType)
            .ToHashSet();

        types.ShouldContain(typeof(HealingCard));
        types.ShouldContain(typeof(FightingCard));
        types.ShouldContain(typeof(StealingCard));
        types.ShouldContain(typeof(CoursedCard));
        types.ShouldContain(typeof(ThornDamageCard));
    }

    [Fact]
    public void AddDomainServices_registers_expected_battle_strategies()
    {
        var services = new ServiceCollection();
        services.AddDomainServices();
        using var sp = services.BuildServiceProvider();

        var types = sp.GetServices<IBattleStrategy>()
            .Select(s => s.SphereType)
            .ToHashSet();

        types.ShouldContain(typeof(BlueSkysphere));
    }
}
