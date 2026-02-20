using Domain.Shared;

namespace Domain.Battles.Duels;

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
public readonly record struct EvasionAdd
{
    public static readonly EvasionAdd None = From(0);

    public int Value { get; }
    private EvasionAdd(int value)
    {
        Value = value;
    }

    public static EvasionAdd From(int value)
    {
        RuleChecker.CheckRule(new CheckAddPercentageRangeRule(value));
        return new EvasionAdd(value);
    }
}

