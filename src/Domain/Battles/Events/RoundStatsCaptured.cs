using Domain.Warriors;

namespace Domain.Battles.Events;

public sealed record RoundStatsCaptured : BattleEventBase
{
    public WarrirorStat Attacker { get; }
    public WarrirorStat Opponent { get; }

    internal RoundStatsCaptured(Warrior attacker, Warrior oponent)
    {
        Attacker = new WarrirorStat(attacker.Name, attacker.Health, attacker.MaxDamage);
        Opponent = new WarrirorStat(oponent.Name, oponent.Health, oponent.MaxDamage);
    }

    public override void Accept(IBattleEventVisitor visitor) => visitor.Visit(this);
}
