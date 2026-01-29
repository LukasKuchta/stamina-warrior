using Domain.Warriors;

namespace Domain.Battles.Events;

public sealed record WarriorDied : IBattleEvent
{
    public WarrirorStat Dead { get; }
    public WarrirorStat Survivor { get; }

    internal WarriorDied(Warrior dead, Warrior survivor)
    {
        Dead = new WarrirorStat(dead.Name, dead.Health, dead.MaxDamage);
        Survivor = new WarrirorStat(survivor.Name, survivor.Health, survivor.MaxDamage);
    }

    public void Accept(IBattleEventVisitor visitor) => visitor.Visit(this);
}
