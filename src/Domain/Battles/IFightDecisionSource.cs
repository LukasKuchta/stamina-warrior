using Domain.ActivationRules;
using Domain.Warriors;

namespace Domain.Battles;

public interface IFightDecisionSource
{
    int PickSlotIndex(int maxCardIndex);
    int PickDamage(int maxDamage);
    bool HitCheck(Warrior attacker);
}
