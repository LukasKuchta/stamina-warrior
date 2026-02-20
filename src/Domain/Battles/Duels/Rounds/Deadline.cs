using Domain.Battles.Duels.Rules;
using Domain.Shared;

namespace Domain.Battles.Duels.Rounds;

public sealed record Deadline : ValueObjectBase
{
    public DateTimeOffset ExpiresAt { get; }

    private Deadline(DateTimeOffset start, TimeSpan duration)
    {
        ExpiresAt = start.Add(duration);
     }

    public bool IsExpired(DateTimeOffset now) => now >= ExpiresAt;
    public TimeSpan Remaining(DateTimeOffset now) => ExpiresAt - now;

    public static Deadline FromStart(DateTimeOffset start, TimeSpan duration)
    {
        CheckRule(new CheckDurationPositiveRule(duration));

        return new Deadline(start, duration);
    }
}
