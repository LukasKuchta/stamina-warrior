using Domain.MagicCards;
using Domain.Shared;

namespace Domain.Battles.Rules;

public sealed class ZeroPowerWiollKillYouRule(Power power) : IBusinessRule
{
    public string Message => "Zero power will kill you!";

    public bool IsBroken()
    {
        return power == Power.Zero;
    }
}
