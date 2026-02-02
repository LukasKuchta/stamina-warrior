
using Domain.Shared;

namespace Domain.MagicCards.Rules;
internal sealed class ValueCanBeBetweenZeoroAndOneRule : IBusinessRule
{
    private readonly float _chance;

    internal ValueCanBeBetweenZeoroAndOneRule(float chance)
    {
        _chance = chance;
    }

    public string Message => "Chance must be between 0 and 1.";

    public bool IsBroken()
    {
        return float.IsNaN(_chance) || _chance < 0.0 || _chance > 1.0;
    }
}
