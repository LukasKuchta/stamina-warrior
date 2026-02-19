using System;
using System.Collections.Generic;
using System.Text;
using Domain.Battles.Events;
using Domain.Shared;

namespace Domain.Battles.Duels;

public sealed record class RoundStatus : ValueObjectBase
{
    public DuelRoundState State { get; }
    public DuelRoundCloseReason Reason { get; }

    private RoundStatus(DuelRoundState state, DuelRoundCloseReason reason)
    {
        State = state;
        Reason = reason;
    }

    public static RoundStatus Open()
    {
        return new RoundStatus(DuelRoundState.Open, DuelRoundCloseReason.NotClosedYet);
    }

    public static RoundStatus Close(DuelRoundCloseReason closeReason)
    {
        var closeState = DuelRoundState.Closed;
        CheckRule(new ClosedStateMustCorelateWithCloseReasonRule(closeState, closeReason));

        return new RoundStatus(closeState, closeReason);
    }
}
