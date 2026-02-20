namespace Domain.Battles.Duels.AttackExchanges;

public record AttackExchangeId
{
    public Guid Value { get; }
    private AttackExchangeId(Guid value)
    {
        Value = value;
    }

    public static AttackExchangeId NewId() => new(Guid.NewGuid());
}
