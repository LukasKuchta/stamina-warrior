namespace Domain.Battles.Events;

public sealed record RoundStarted : IBattleEvent
{
    internal RoundStarted(int roundNumber)
    {
        Round = roundNumber;
    }

    public int Round { get; }

    public void Accept(IBattleEventVisitor visitor) => visitor.Visit(this);
}
