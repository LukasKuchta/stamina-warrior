namespace Domain.Battles.Duels;

public sealed record DuelId(Guid Value)
{
    public static DuelId NewId() => new(Guid.NewGuid());
}
