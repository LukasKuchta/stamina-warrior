namespace Domain.Battles.Duels;

public record RoundNumber
{
    public readonly static RoundNumber Zero = new RoundNumber(0);

    public int Value { get; private set; }
    private RoundNumber(int value)
    {
        Value = value;
    }

    internal RoundNumber Next()
    {
        return FromValue(Value + 1);
    }

    internal static RoundNumber FromValue(int value) => new RoundNumber(value);
}
