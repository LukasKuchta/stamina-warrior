namespace Domain.Battles.Duels.Rounds;

public sealed record DuelRoundId
{
    public Guid Value { get; }
    private DuelRoundId(Guid value)
    {
        Value = value;
    }
    public static DuelRoundId NewId() => new(Guid.NewGuid());
}
