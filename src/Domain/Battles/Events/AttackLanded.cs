using Domain.Warriors;

namespace Domain.Battles.Events;

public sealed record AttackLanded : IBattleEvent
{
    internal AttackLanded(Warrior atttacker, Warrior oponent, int damage) 
    {
        AttackerName = atttacker.Name;
        OponentName = oponent.Name;
        Damage = damage;        
    }

    public string AttackerName { get; }
    public string OponentName { get; }
    public int Damage { get; }

    public void Accept(IBattleEventVisitor visitor) => visitor.Visit(this);
}
