
using Domain.Shared;

namespace Domain.MagicCards.Rules;
public sealed class MagicPowerCanBePositiveRule : IBusinessRule
{
    private readonly float _magicPower;

    internal MagicPowerCanBePositiveRule(float magicPower)
    {
        _magicPower = magicPower;
    }

    public string Message => "Magic power cannot be negative.";

    public bool IsBroken()
    {
        return _magicPower < 0f;
    }
}
