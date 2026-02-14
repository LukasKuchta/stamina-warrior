using Domain.Battles;
using Domain.RandomSources;

namespace Domain.ActivationRules;

public sealed class ChanceActivationRuleEvaluator(IRandomSource randomSource) : ActivationRuleEvaluator<ChanceActivationRule>
{
    public override bool Matches(ChanceActivationRule rule, AttackContext ctx)
    {
        return randomSource.Succeeds(rule.chance);
    }
}


