using Domain.ActivationRules;

namespace Domain.MagicCards.Cards;

public sealed record ThornCard : MagicCardBase
{
    public ThornCard(Power power) : base("Thorn trap")
    {
        Power = power;
    }
    public Power Power { get; }
}
