namespace Domain.Battles.Events;

public sealed record RoundStarted : BattleEventBase
{
    internal RoundStarted(int roundNumber)
    {
        Round = roundNumber;
    }

    public int Round { get; }

    public override void Accept(IBattleEventVisitor visitor) => visitor.Visit(this);
}
