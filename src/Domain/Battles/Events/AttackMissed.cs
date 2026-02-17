using Domain.Warriors;

namespace Domain.Battles.Events;

public sealed record AttackMissed : BattleEventBase
{
    internal AttackMissed(Warrior atttacker)
    {
        AttackerName = atttacker.Name;        
    }

    public string AttackerName { get; }

    public override void Accept(IBattleEventVisitor visitor) => visitor.Visit(this);
}
