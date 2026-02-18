
using Domain.ActivationRules;
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
        services.AddSingleton<IBattleStrategy, BlueSkyBattleStrategy>();        

        services.AddSingleton<IBattleEndEventBuilder, BattleEndEventBuilder>();

        services.AddSingleton<IMagicCardStrategy<HealingCard>, HealingCardStrategy>();
        services.AddSingleton<IMagicCardStrategy<FightingCard>, FightingCardStrategy>();
        services.AddSingleton<IMagicCardStrategy<StealingCard>, StealingCardStrategy>();
        services.AddSingleton<IMagicCardStrategy<ThornCard>, ThornDamageStrategy>();
        services.AddSingleton<IMagicCardStrategy<CoursedCard>, CoursedCardStrategy>();

        services.AddSingleton(sp => (IMagicCardStrategy)sp.GetRequiredService<IMagicCardStrategy<HealingCard>>());
        services.AddSingleton(sp => (IMagicCardStrategy)sp.GetRequiredService<IMagicCardStrategy<FightingCard>>());
        services.AddSingleton(sp => (IMagicCardStrategy)sp.GetRequiredService<IMagicCardStrategy<StealingCard>>());
        services.AddSingleton(sp => (IMagicCardStrategy)sp.GetRequiredService<IMagicCardStrategy<CoursedCard>>());
        services.AddSingleton(sp => (IMagicCardStrategy)sp.GetRequiredService<IMagicCardStrategy<ThornCard>>());

        services.AddSingleton<IActivationRuleEvaluator<ChanceActivationRule>, ChanceActivationRuleEvaluator>();
        services.AddSingleton<IActivationRuleEvaluator<ConditionActivationRule>, ConditionActivationRuleEvaluator>();

        services.AddSingleton(sp => (IActivationRuleEvaluator)sp.GetRequiredService<IActivationRuleEvaluator<ChanceActivationRule>>());
        services.AddSingleton(sp => (IActivationRuleEvaluator)sp.GetRequiredService<IActivationRuleEvaluator<ConditionActivationRule>>());

        services.AddSingleton<IActivationRuleEvaluatorSelector, ActivationRuleEvaluatorSelector>();

        services.AddSingleton<IRandomSource, RandomSource>();
        services.AddSingleton<IFightDecisionSource, FightDecisionSource>();

        services.AddSingleton<IMagicCardStrategyFactory, MagicCardStrategyFactory>();

        return services;
    }
}
