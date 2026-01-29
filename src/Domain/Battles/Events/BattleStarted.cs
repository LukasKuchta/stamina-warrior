using Domain.Warriors;

namespace Domain.Battles.Events;

public sealed record BattleStarted : IBattleEvent
{
    internal BattleStarted(Warrior Attacker, Warrior Opponent)
    {
        this.Attacker = new WarrirorStat(Attacker.Name, Attacker.Health, Attacker.MaxDamage);
        Oponent = new WarrirorStat(Opponent.Name, Opponent.Health, Opponent.MaxDamage);
    }

    public WarrirorStat Attacker { get; }
    public WarrirorStat Oponent { get; }

    public void Accept(IBattleEventVisitor visitor) => visitor.Visit(this);
}
