using Domain.Battles.Duels.Rounds;
using Domain.Shared;

namespace Domain.Battles.Duels.Rules;

public sealed class AllowOnlyTransitFromOpenToCloseRule(DuelRoundState state) : IBusinessRule
{
    public string Message => "Only transit from Open to Close state is allowed.";

    public bool IsBroken()
    {
        return state != DuelRoundState.Open;
    }
}
