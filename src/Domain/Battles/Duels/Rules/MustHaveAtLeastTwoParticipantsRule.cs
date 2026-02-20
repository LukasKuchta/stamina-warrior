using System.Collections.Generic;
using Domain.Shared;

namespace Domain.Battles.Duels.Rules;

public sealed class MustHaveAtLeastTwoParticipantsRule(int warriors) : IBusinessRule
{
    public string Message => "At least two warriors must participate in a duel round.";

    public bool IsBroken()
    {
        return warriors < 2;
    }
}
