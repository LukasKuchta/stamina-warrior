using Domain.MagicCards;

namespace Domain.Battles;

public interface IFightDecisionSource
{
    int PickCardIndex(int maxCardIndex);
    int PickBaseDamage(int maxDamage);

    bool TryActivate(Chance activationChance);
}
