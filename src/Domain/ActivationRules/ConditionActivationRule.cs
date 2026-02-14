using Domain.Battles;

namespace Domain.ActivationRules;

public sealed record ConditionActivationRule(Func<AttackContext, bool> Condition) : ActivationRuleBase;


