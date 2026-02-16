using Domain.ActivationRules;

namespace Domain.Battles;

public interface IFightDecisionSource
{
    int PickSlotIndex(int maxCardIndex);
    int PickBaseDamage(int maxDamage);    
}
