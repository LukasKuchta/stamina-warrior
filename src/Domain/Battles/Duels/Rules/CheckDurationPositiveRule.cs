using Domain.Shared;

namespace Domain.Battles.Duels.Rules;

public sealed class CheckDurationPositiveRule(TimeSpan duration) : IBusinessRule
{
    public string Message => "Duration cannot be negative!";

    public bool IsBroken()
    {
        return duration.TotalSeconds > 0;
    }
}
