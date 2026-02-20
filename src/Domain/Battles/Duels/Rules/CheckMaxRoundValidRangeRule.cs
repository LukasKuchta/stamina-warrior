using Domain.Shared;

namespace Domain.Battles.Duels.Rules;

public sealed class CheckMaxRoundValidRangeRule(int value) : IBusinessRule
{
    private const int MaxAllowedRounds = 100;
    public string Message => $"Max round must be greater than 0 and less than or equal to {MaxAllowedRounds}";

    public bool IsBroken()
    {
        return value <= 0 || value > MaxAllowedRounds;
    }
}
