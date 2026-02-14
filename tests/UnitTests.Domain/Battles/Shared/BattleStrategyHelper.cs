using Domain.ActivationRules;
using Domain.Battles;
using Domain.Battles.Strategies;
using Domain.MagicCards;

namespace Domain.UnitTests.Battles.Shared;

internal sealed class BattleStrategyHelper
{
    public static IBattleStrategy CreateDefaultBlueSky()
    {
        return new BlueSkyBattleStrategy(
             new MagicCardStrategyFactory([]),
             new EchoDecisionSource(0),
             new ActivationRuleEvaluatorSelector([new ChanceActivationRuleEvaluator(new FakeIRandomSource())]),
             new BattleEndEventBuilder());
    }
}
