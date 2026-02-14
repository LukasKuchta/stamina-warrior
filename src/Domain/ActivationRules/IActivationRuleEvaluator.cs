using Domain.Battles;
using Domain.RandomSources;
using Domain.Shared;
using Domain.Warriors;

namespace Domain.ActivationRules;

public interface IActivationRuleEvaluator<in TRule> where TRule : ActivationRuleBase
{
    bool Matches(TRule rule, AttackContext ctx);
}

public interface IActivationRuleEvaluator 
{
    Type RuleType { get; }
    bool Matches(ActivationRuleBase rule, AttackContext ctx);
}

public record AttackContext(Warrior Attacker, Warrior Oponent);


