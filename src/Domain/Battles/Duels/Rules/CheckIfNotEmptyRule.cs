using Domain.Shared;
using Domain.Warriors;

namespace Domain.Battles.Duels.Rules;

public sealed class CheckIfNotEmptyRule(IReadOnlyCollection<WarriorId> participants) : IBusinessRule
{
    public string Message => "Warriors collection must not be empty.";

    public bool IsBroken()
    {
        return participants.Count == 0;
    }
}
