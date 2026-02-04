namespace Domain.MagicCards.Cards;

public sealed record ThornDamageCard : MagicCardBase
{
    public ThornDamageCard(Chance activationChance, Power power) : base("Card of thorn's damage", activationChance)
    {
        Power = power;
    }
    public Power Power { get; }
}
