using Domain.Shared;

namespace Domain.Battles.Duels;

public sealed class OpenStateMustCorelateWithNotClosedYetRule(DuelRoundState roundState, DuelRoundCloseReason closeReason) : IBusinessRule
{
    public string Message => "Open round must have NotClosedYet reason.";

    public bool IsBroken()
    {
        return roundState == DuelRoundState.Open && closeReason != DuelRoundCloseReason.NotClosedYet;
    }
}
