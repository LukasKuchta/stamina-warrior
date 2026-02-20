namespace Domain.Battles.Duels.Mods.DamageMods;

public readonly record struct DamageAdd(int Value)
{
    public static readonly DamageAdd None = new(0);
}
