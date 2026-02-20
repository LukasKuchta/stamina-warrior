using Domain.Battles.Duels.Rules;
using Domain.Shared;

namespace Domain.Battles.Duels.Mods.AccuracyMods;

public readonly record struct AccuracyAdd
{
    public static readonly AccuracyAdd None = From(0);

    public int Value { get; }
    private AccuracyAdd(int value)
    {
        Value = value;
    }

    public static AccuracyAdd From(int value)
    {
        RuleChecker.CheckRule(new CheckAddPercentageRangeRule(value));
        return new AccuracyAdd(value);
    }

}

