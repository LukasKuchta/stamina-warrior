using Domain.Battles.Duels.DuelWarriorStates;
using Domain.Battles.Duels.Mods;

namespace Domain.Battles.Duels.Mods.EvasionMods;

internal sealed record EvasionMod(EvasionAdd Add) : ModBase
{
    public static readonly EvasionMod None = new(EvasionAdd.None);

    public Evasion Apply(Evasion @base)
    {
        return @base.Add(Add);
    }
}
