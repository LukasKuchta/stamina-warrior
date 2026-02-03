using Domain.Warriors;

namespace Domain.Battles.Events;

public sealed record BattleStarted : IBattleEvent
{
    internal BattleStarted(Warrior attacker, Warrior Opponent)
    {
        Attacker = new WarrirorStat(attacker.Name, attacker.Health, attacker.MaxDamage);
        Oponent = new WarrirorStat(Opponent.Name, Opponent.Health, Opponent.MaxDamage);
    }

    public WarrirorStat Attacker { get; }
    public WarrirorStat Oponent { get; }

    public void Accept(IBattleEventVisitor visitor) => visitor.Visit(this);
}
