using System;
using System.Collections.Generic;
using System.Text;
using Domain.Battles;
using Domain.MagicCards;

namespace Domain.UnitTests.Battles.Shared;

internal sealed class EchoDecisionSource(int cardIndex) : IFightDecisionSource
{
    public int PickBaseDamage(int maxDamage)
    {
        return maxDamage;
    }

    public int PickCardIndex(int maxCardIndex)
    {
        return cardIndex;
    }

    public bool TryActivate(Chance activationChance)
    {
        return true;
    }
}
