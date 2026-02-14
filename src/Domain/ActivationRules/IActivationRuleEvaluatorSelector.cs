namespace Domain.ActivationRules;

public interface IActivationRuleEvaluatorSelector
{
    IActivationRuleEvaluator SelectBy(ActivationRuleBase rule);
}


