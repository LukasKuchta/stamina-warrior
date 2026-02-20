using Domain.Battles.Duels.Mods.EvasionMods;
using Domain.Battles.Duels.Rules;
using Domain.Shared;

namespace Domain.Battles.Duels.DuelWarriorStates;

public readonly record struct Evasion
{
    public const int Min = 0;
    public const int Max = 100;

    public int Value { get; }
    private Evasion(int value) => Value = value;
    
    public Evasion Add(EvasionAdd add)
    {
        var next = Value + add.Value;
        next = Math.Clamp(next, Min, Max);
        return From(next);
    }

    public static Evasion From(int value)
    {
        RuleChecker.CheckRule(new CheckPercentageRangeRule(value));
        return new Evasion(value);
    }
}
