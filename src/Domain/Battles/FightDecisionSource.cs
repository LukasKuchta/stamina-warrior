using Domain.MagicCards;
using Domain.RandomSources;

namespace Domain.Battles;

public sealed class FightDecisionSource(IRandomSource chanceService) : IFightDecisionSource
{
    public int PickBaseDamage(int maxDamage)
    {
        return chanceService.NextIntInclusive(maxDamage);
    }

    public int PickCardIndex(int maxCardIndex)
    {
        return chanceService.NextIntInclusive(maxCardIndex);
    }

    public bool TryActivate(Chance activationChance)
    {
        return chanceService.Succeeds(activationChance);
    }
}
