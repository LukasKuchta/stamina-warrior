namespace Domain.Battles.Duels;

public sealed record DuelRoundId
{
    public Guid Value { get; }
    private DuelRoundId(Guid value)
    {
        Value = value;
    }
    public static DuelRoundId NewId() => new(Guid.NewGuid());
}
