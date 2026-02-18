using Domain.Shared;

namespace Domain.Battles.Duels;

public sealed class AllowOnlyTransitFromOpenToCLoseRule(DuelRoundState state) : IBusinessRule
{
    public string Message => "Only transit from Open to Close state is allowed.";

    public bool IsBroken()
    {
        return state != DuelRoundState.Open;
    }
}
