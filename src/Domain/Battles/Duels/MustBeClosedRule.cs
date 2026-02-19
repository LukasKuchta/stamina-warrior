using Domain.Shared;

namespace Domain.Battles.Duels;

public sealed class MustBeClosedRule(DuelRoundState state) : IBusinessRule
{
    public string Message => "Current round must be closed to open the next one.";

    public bool IsBroken()
    {
        return state != DuelRoundState.Open;
    }
}
