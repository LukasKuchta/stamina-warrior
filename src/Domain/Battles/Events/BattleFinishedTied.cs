using Domain.Warriors;

namespace Domain.Battles.Events;

public sealed record BattleFinishedTied : IBattleEvent
{
    internal BattleFinishedTied(Warrior warrior1, Warrior warrior2)
    {
        Warrior1Name = warrior1.Name;
        Warrior2Name = warrior2.Name;
    }

    public string Warrior1Name { get; }
    public string Warrior2Name { get; }

    public void Accept(IBattleEventVisitor visitor) => visitor.Visit(this);
}
