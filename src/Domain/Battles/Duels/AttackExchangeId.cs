namespace Domain.Battles.Duels;

public record AttackExchangeId
{
    public Guid Value { get; }
    private AttackExchangeId(Guid value)
    {
        Value = value;
    }

    public static AttackExchangeId NewId() => new(Guid.NewGuid());
}
