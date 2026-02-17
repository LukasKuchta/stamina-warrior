using Domain.ActivationRules;
using Domain.Warriors;

namespace Domain.Battles.Events;


public sealed record CardDrawn : BattleEventBase
{
    internal CardDrawn(Warrior cardHolder, string cardName, ActivationRuleBase rule)
    {        
        CardHolder = new WarrirorStat(cardHolder.Name, cardHolder.Health, cardHolder.MaxDamage);
        CardName = cardName;
        ActivationRule = rule.GetType().Name;
    }

    public string ActivationRule { get; }    
    public WarrirorStat CardHolder { get; }
    public string CardName { get; }

    public override void Accept(IBattleEventVisitor visitor) => visitor.Visit(this);
}
