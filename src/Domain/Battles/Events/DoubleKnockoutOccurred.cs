using Domain.Warriors;

namespace Domain.Battles.Events;

public sealed record DoubleKnockoutOccurred : IBattleEvent
{
    public WarrirorStat Attacker { get; }
    public WarrirorStat Oponent { get; }

    internal DoubleKnockoutOccurred(Warrior attacker, Warrior oponent)
    {
        Attacker = new WarrirorStat(attacker.Name, attacker.Health, attacker.MaxDamage);
        Oponent = new WarrirorStat(oponent.Name, oponent.Health, oponent.MaxDamage);
    }

    public void Accept(IBattleEventVisitor visitor) => visitor.Visit(this);
}
