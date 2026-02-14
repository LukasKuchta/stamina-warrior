using Domain.Battles;

namespace Domain.ActivationRules;

public abstract class ActivationRuleEvaluator<TRule> : IActivationRuleEvaluator<TRule>, IActivationRuleEvaluator
    where TRule : ActivationRuleBase
{
    public Type RuleType => typeof(TRule);

    public abstract bool Matches(TRule rule, AttackContext ctx);

    public bool Matches(ActivationRuleBase rule, AttackContext ctx) => Matches((TRule)rule, ctx);
}


