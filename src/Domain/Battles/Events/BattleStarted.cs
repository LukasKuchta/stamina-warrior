using Domain.Warriors;

namespace Domain.Battles.Events;

public sealed record BattleStarted : BattleEventBase
{
    internal BattleStarted(Warrior attacker, Warrior Opponent, DateTimeOffset startedAt)
    {
        StartedAt = startedAt;
        Attacker = new WarrirorStat(attacker.Name, attacker.Health, attacker.MaxDamage);
        Oponent = new WarrirorStat(Opponent.Name, Opponent.Health, Opponent.MaxDamage);
    }

    public DateTimeOffset StartedAt { get; }
    public WarrirorStat Attacker { get; }
    public WarrirorStat Oponent { get; }

    public override void Accept(IBattleEventVisitor visitor) => visitor.Visit(this);
}
