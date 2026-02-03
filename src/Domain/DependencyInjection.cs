
using Domain.Battles;
using Domain.Battles.Spheres;
using Domain.Battles.Strategies;
using Domain.MagicCards;
using Domain.MagicCards.Cards;
using Domain.MagicCards.Strategies;
using Domain.RandomSources;
using Microsoft.Extensions.DependencyInjection;

namespace Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddSingleton<IBattleStrategy<BlueSkysphere>, BlueSkyBattleStrategy>();
        services.AddSingleton(sp => (IBattleStrategy)sp.GetRequiredService<IBattleStrategy<BlueSkysphere>>());
        services.AddSingleton<IBattleStrategyFactory, BattleStrategyFactory>();
        services.AddSingleton<IBattleEndEventBuilder, BattleEndEventBuilder>();

        services.AddSingleton<IMagicCardStrategy<HealingCard>, HealingCardStrategy>();
        services.AddSingleton<IMagicCardStrategy<FightingCard>, FightingCardStrategy>();
        services.AddSingleton<IMagicCardStrategy<StealingCard>, StealingCardStrategy>();
        services.AddSingleton<IMagicCardStrategy<ThornDamageCard>, ThornDamageStrategy>();
        services.AddSingleton<IMagicCardStrategy<CoursedCard>, CoursedCardStrategy>();

        services.AddSingleton(sp => (IMagicCardStrategy)sp.GetRequiredService<IMagicCardStrategy<HealingCard>>());
        services.AddSingleton(sp => (IMagicCardStrategy)sp.GetRequiredService<IMagicCardStrategy<FightingCard>>());
        services.AddSingleton(sp => (IMagicCardStrategy)sp.GetRequiredService<IMagicCardStrategy<StealingCard>>());
        services.AddSingleton(sp => (IMagicCardStrategy)sp.GetRequiredService<IMagicCardStrategy<CoursedCard>>());
        services.AddSingleton(sp => (IMagicCardStrategy)sp.GetRequiredService<IMagicCardStrategy<ThornDamageCard>>());

        services.AddSingleton<IRandomSource, RandomSource>();
        services.AddSingleton<IFightDecisionSource, FightDecisionSource>();

        services.AddSingleton<IMagicCardStrategyFactory, MagicCardStrategyFactory>();

        return services;
    }
}
