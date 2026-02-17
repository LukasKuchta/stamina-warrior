using System.Threading.Channels;
using Domain.ActivationRules;
using Domain.RandomSources;
using Domain.Warriors;

namespace Domain.Battles;

public sealed class FightDecisionSource(IRandomSource chanceService) : IFightDecisionSource
{
    public int PickDamage(int maxDamage)
    {
        return chanceService.NextIntInclusive(maxDamage);
    }

    public int PickSlotIndex(int maxCardIndex)
    {
        return chanceService.NextIntInclusive(maxCardIndex);
    }

    public bool HitCheck(Warrior attacker)
    {
        float @base = 0.75f;
        float scale = 0.01f;
        float chance = @base + (attacker.Accuracy - attacker.Evasion) * scale;

        return chanceService.Succeeds(Chance.FromValue(chance));
    }
}
