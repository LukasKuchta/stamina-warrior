using Domain.Shared;
using Domain.Warriors;

namespace Domain.Battles.Duels;

public sealed class MustBelongToParticipantsRule(RoundParticipantsSnapshot participants, WarriorId markedAsReady) : IBusinessRule
{
    public string Message => "Warrior marked as ready must be one of the participants of the round.";

    public bool IsBroken()
    {
        return !participants.Snapshot.Contains(markedAsReady);
    }
}
