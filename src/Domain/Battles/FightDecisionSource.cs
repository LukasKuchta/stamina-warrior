using Domain.ActivationRules;
using Domain.RandomSources;

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
}
