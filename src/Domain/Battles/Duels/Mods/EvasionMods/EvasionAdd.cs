using Domain.Battles.Duels.Rules;
using Domain.Shared;

namespace Domain.Battles.Duels.Mods.EvasionMods;

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

