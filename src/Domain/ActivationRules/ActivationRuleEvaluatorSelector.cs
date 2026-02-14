namespace Domain.ActivationRules;

public sealed class ActivationRuleEvaluatorSelector : IActivationRuleEvaluatorSelector
{
    private readonly IDictionary<Type, IActivationRuleEvaluator> _evaluators;
    public ActivationRuleEvaluatorSelector(IEnumerable<IActivationRuleEvaluator> evaluators)
    {
        _evaluators = evaluators.ToDictionary(s => s.RuleType);
    }
    public IActivationRuleEvaluator SelectBy(ActivationRuleBase rule)
    {
        return _evaluators[rule.GetType()];
    }
}


