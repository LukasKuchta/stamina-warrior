using Domain.ActivationRules;

namespace Domain.MagicCards.Cards;

public sealed record CoursedCard : MagicCardBase
{
    public CoursedCard(Power power) : base("Coursed card")
    {
        Power = power;
    }

    public Power Power { get; }
}
