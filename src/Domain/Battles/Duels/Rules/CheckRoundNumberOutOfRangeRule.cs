using Domain.Battles.Duels.Rounds;
using Domain.Shared;

namespace Domain.Battles.Duels.Rules;

public sealed class CheckRoundNumberOutOfRangeRule(RoundNumber next, MaxRound maxRound) : IBusinessRule
{
    public string Message => $"New round: {next} out of the ragne 0 - {maxRound.Value}";

    public bool IsBroken()
    {
        return next.Value <= 0 || next.Value > maxRound.Value;
    }
}
