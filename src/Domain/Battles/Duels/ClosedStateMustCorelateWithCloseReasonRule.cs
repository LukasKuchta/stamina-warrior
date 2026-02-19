using Domain.Shared;

namespace Domain.Battles.Duels;

public sealed class ClosedStateMustCorelateWithCloseReasonRule(DuelRoundState roundState, DuelRoundCloseReason closeReason) : IBusinessRule
{
    public string Message => "Closed round must have a close reason.";

    public bool IsBroken()
    {
        return roundState == DuelRoundState.Closed && closeReason == DuelRoundCloseReason.NotClosedYet;
    }
}
