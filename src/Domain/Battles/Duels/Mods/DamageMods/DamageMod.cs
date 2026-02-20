using System;
using Domain.Battles.Duels.DuelWarriorStates;

namespace Domain.Battles.Duels.Mods.DamageMods;

internal sealed record DamageMod(DamageAdd Add, DamageMul Mul) : ModBase
{
    public static readonly DamageMod None = new(DamageAdd.None, DamageMul.None);

    public Damage Apply(Damage baseDamage)
    {
        var afterAdd = baseDamage.Value + Add.Value;
        if (afterAdd < 0)
        {
            afterAdd = 0;
        }

        var afterMul = Mul.ApplyTo(afterAdd);
        if (afterMul < 0)
        {
            afterMul = 0;
        }

        return Damage.From(afterMul);
    }
}
