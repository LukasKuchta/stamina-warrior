using Domain.Warriors;

namespace Domain.Battles.Events;

public sealed record WarriorDied : BattleEventBase
{
    public WarrirorStat DeadMan { get; }
    public WarrirorStat Survivor { get; }

    internal WarriorDied(Warrior dead, Warrior survivor)
    {
        DeadMan = new WarrirorStat(dead.Name, dead.Health, dead.MaxDamage);
        Survivor = new WarrirorStat(survivor.Name, survivor.Health, survivor.MaxDamage);
    }

    public override void Accept(IBattleEventVisitor visitor) => visitor.Visit(this);
}
