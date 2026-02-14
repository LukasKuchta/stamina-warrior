using Domain.Battles;

namespace Domain.ActivationRules;

public sealed class ConditionActivationRuleEvaluator : ActivationRuleEvaluator<ConditionActivationRule>
{
    public override bool Matches(ConditionActivationRule rule, AttackContext ctx)
    {
        return rule.Condition.Invoke(ctx);
    }
}


