using Domain.Battles.Duels.Rules;
using Domain.Shared;

namespace Domain.Battles.Duels.DuelWarriorStates;

public readonly record struct Health
{
    public int Value { get; }

    private Health(int value) => Value = value;

    public static Health From(int value)
    {
        RuleChecker.CheckRule(new CannotBeNegativeRule(value, nameof(Health)));
        return new Health(value);
    }

    public Health Take(Damage dmg) => From(Math.Max(0, Value - dmg.Value));
    public Health Add(HealAmount heal) => From(Value + heal.Value);
}
