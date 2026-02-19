using Domain.Shared;
using Domain.Warriors;

namespace Domain.Battles.Duels;

public sealed class CheckAllReadyRule(RoundParticipantsSnapshot participants, IEnumerable<WarriorId> readyWarriors) : IBusinessRule
{
    public string Message => "Äll members must be ready.";

    public bool IsBroken()
    {
        return !participants.CompareMembers(readyWarriors);
    }
}
