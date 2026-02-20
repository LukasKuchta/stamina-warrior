using Domain.Battles.Duels.Mods.DamageMods;
using Domain.Battles.Duels.Rules;
using Domain.Shared;

namespace Domain.Battles.Duels.DuelWarriorStates;

public readonly record struct Damage
{
    public int Value { get; }

    private Damage(int value) => Value = value;

    public Damage Add(DamageAdd add) => From(Value + add.Value);

    public static Damage From(int value)
    {
        RuleChecker.CheckRule(new CannotBeNegativeRule(value, nameof(Damage)));
        return new Damage(value);
    }
}
