namespace Domain.Battles.Duels;

public record RoundNumber
{
    public readonly static RoundNumber First = new RoundNumber(1);

    public int Value { get; private set; }
    private RoundNumber(int value)
    {
        Value = value;
    }

    public RoundNumber Next()
    {
        return FromValue(Value + 1);
    }

    public static RoundNumber FromValue(int value) => new RoundNumber(value);
}
