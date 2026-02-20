using Domain.Battles.Duels.Mods.AccuracyMods;
using Domain.Battles.Duels.Rules;
using Domain.Shared;

namespace Domain.Battles.Duels.DuelWarriorStates;

public readonly record struct Accuracy
{
    public const int Min = 0;
    public const int Max = 100;

    public int Value { get; }
    private Accuracy(int value) => Value = value;

    public Accuracy Add(AccuracyAdd add) 
    {
        var next = Value + add.Value;
        next = Math.Clamp(next, Min, Max);
        return From(next);
    }    

    public static Accuracy From(int value)
    {
        RuleChecker.CheckRule(new CheckPercentageRangeRule(value));
        return new Accuracy(value);
    }
}
