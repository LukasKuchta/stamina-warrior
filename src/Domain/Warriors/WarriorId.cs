namespace Domain.Warriors;

public sealed record WarriorId
{
    public Guid Value { get; }
    private WarriorId(Guid value)
    {
        Value = value;
    }

    public static WarriorId New()
    {
        return new WarriorId(Guid.NewGuid());
    }
}
