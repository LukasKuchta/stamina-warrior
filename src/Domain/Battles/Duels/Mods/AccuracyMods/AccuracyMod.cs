using Domain.Battles.Duels.DuelWarriorStates;

namespace Domain.Battles.Duels.Mods.AccuracyMods;

internal sealed record AccuracyMod(AccuracyAdd Add) : ModBase
{
    public static readonly AccuracyMod None = new(AccuracyAdd.None);

    public Accuracy Apply(Accuracy @base)
    {
        return @base.Add(Add);
    }
}
