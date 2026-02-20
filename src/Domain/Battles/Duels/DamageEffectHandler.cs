namespace Domain.Battles.Duels;

internal sealed class DamageEffectHandler : ModEffectHandlerBase<DamageEffect>
{
    public override void Amplify(Modifiers mods, DuelWarriorState self, DuelWarriorState opponent, DamageEffect effect)
    {
        // compute damage mod based on the effect and add it to mods
        //mods.Add(new DamageMod());
    }
}

