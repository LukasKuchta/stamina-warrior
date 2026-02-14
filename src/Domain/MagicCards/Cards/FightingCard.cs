using Domain.ActivationRules;

namespace Domain.MagicCards.Cards;

public sealed record FightingCard : MagicCardBase
{
    public FightingCard(Power power) : base("Fighting card")
    {
        Power = power;
    }

    public Power Power { get; }
}

