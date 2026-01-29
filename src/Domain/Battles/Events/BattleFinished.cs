using Domain.Warriors;

namespace Domain.Battles.Events;

public sealed record BattleFinished : IBattleEvent
{
    public WarrirorStat Winner { get; }
    public WarrirorStat Looser { get; }

    internal BattleFinished(Warrior winner, Warrior looser)
    {
        Winner = new WarrirorStat(winner.Name, winner.Health, winner.MaxDamage);
        Looser = new WarrirorStat(looser.Name, looser.Health, looser.MaxDamage);
    }

    public void Accept(IBattleEventVisitor visitor) => visitor.Visit(this);
}
