
using Domain.MagicCards.Rules;
using Domain.Shared;

namespace Domain.MagicCards;
public sealed record Chance : ValueObjectBase
{
    public static readonly Chance Never = new(0f);
    public static readonly Chance Always = new(1f);
    public static readonly Chance CoinFlip = new(0.5f);

    public float Value { get; }

    private Chance(float value)
    {
        Value = value;
    }

    public bool IsNone()
    {
        return this == Never;
    }

    public bool IsAlways()
    {
        return this == Always;
    }

    public static Chance FromPercentage(float value)
    {
        CheckRule(new ChanceCanBeBetweenZeoroAndOneRule(value));

        return new Chance(value);
    }  
}
