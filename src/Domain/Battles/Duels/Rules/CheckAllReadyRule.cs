using Domain.Battles.Duels.Rounds;
using Domain.Shared;
using Domain.Warriors;

namespace Domain.Battles.Duels.Rules;

public sealed class CheckAllReadyRule(RoundParticipantsSnapshot participants, IEnumerable<WarriorId> readyWarriors) : IBusinessRule
{
    public string Message => "Äll members must be ready.";

    public bool IsBroken()
    {
        return !participants.CompareMembers(readyWarriors);
    }
}
