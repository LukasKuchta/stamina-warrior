using Domain.Battles.Duels.Rules;
using Domain.Shared;

namespace Domain.Battles.Duels.DuelWarriorStates;

public readonly record struct HealAmount
{
    public int Value { get; }

    private HealAmount(int value) => Value = value;

    public static HealAmount From(int value)
    {
        RuleChecker.CheckRule(new CannotBeNegativeRule(value, nameof(HealAmount)));
        return new HealAmount(value);
    }
}
