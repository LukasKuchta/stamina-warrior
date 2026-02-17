using Domain.ActivationRules;

namespace Domain.Battles;

public interface IFightDecisionSource
{
    int PickSlotIndex(int maxCardIndex);
    int PickDamage(int maxDamage);
}
