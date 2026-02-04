
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

    public bool IsNever()
    {
        return this == Never;
    }

    public bool IsAlways()
    {
        return this == Always;
    }

    public static Chance FromValue(float value)
    {
        CheckRule(new ValueCanBeBetweenZeoroAndOneRule(value));

        return new Chance(value);
    }  
}
