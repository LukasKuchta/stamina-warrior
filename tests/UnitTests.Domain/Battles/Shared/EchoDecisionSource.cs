using Domain.ActivationRules;
using Domain.Battles;

namespace Domain.UnitTests.Battles.Shared;

internal sealed class EchoDecisionSource(int cardIndex) : IFightDecisionSource
{
    public int PickDamage(int maxDamage)
    {
        return maxDamage;
    }

    public int PickSlotIndex(int maxCardIndex)
    {
        return cardIndex;
    }
}

internal sealed class SequnceDecisionSource(int[] sequnce) : IFightDecisionSource
{
    private int currentIndex;

    public int PickDamage(int maxDamage)
    {
        return maxDamage;
    }

    public int PickSlotIndex(int maxCardIndex)
    {
        if (currentIndex < sequnce.Length)
        {
            return sequnce[currentIndex++];
        }

        return 10;
    }
}
