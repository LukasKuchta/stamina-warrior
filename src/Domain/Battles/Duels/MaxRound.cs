using Domain.Battles.Duels.Rules;
using Domain.Shared;

namespace Domain.Battles.Duels;

public record MaxRound : ValueObjectBase
{
    public int Value { get; }

    private MaxRound(int value)
    {
        Value = value;
    }

    public static MaxRound FromValue(int value)
    {
        CheckRule(new CheckMaxRoundValidRangeRule(value));

        return new MaxRound(value);
    }
}
