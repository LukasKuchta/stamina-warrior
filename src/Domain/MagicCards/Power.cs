
using Domain.MagicCards.Rules;
using Domain.Shared;

namespace Domain.MagicCards;

public sealed record Power : ValueObjectBase
{
    public static readonly Power Zero = new(0);

    public float Value { get; }

    private Power(float value)
    {
        Value = value;
    }

    public bool NoPower => this == Power.Zero;

    public bool HasPower => !NoPower;

    public static Power FromValue(float value)
    {
        CheckRule(new MagicPowerCanBePositiveRule(value));

        return new Power(value);
    }
}
