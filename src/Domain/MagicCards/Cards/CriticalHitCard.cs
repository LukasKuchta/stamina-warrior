using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.MagicCards.Cards;

public sealed record CriticalHitCard : MagicCardBase
{
    public CriticalHitCard(Power power) : base("Critical hit")
    {
        Power = power;
    }

    public Power Power { get; }
}
