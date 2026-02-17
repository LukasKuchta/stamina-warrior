using Domain.ActivationRules;
using Domain.Battles;
using Domain.Warriors;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;

namespace Domain.UnitTests.Battles.Shared;

internal sealed class EchoDecisionSource(int cardIndex, bool hitCheck = true) : IFightDecisionSource
{
    public bool HitCheck(Warrior attacker)
    {
        return hitCheck;
    }

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

    public bool HitCheck(Warrior attacker)
    {
        return true;
    }

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
