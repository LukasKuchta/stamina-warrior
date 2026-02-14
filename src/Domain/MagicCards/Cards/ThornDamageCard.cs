using Domain.ActivationRules;

namespace Domain.MagicCards.Cards;

public sealed record ThornDamageCard : MagicCardBase
{
    public ThornDamageCard(Power power) : base("Card of thorn's damage")
    {
        Power = power;
    }
    public Power Power { get; }
}
