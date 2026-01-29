namespace Domain.MagicCards.Cards;

public sealed record ThornDamageCard : MagicCardBase
{
    public ThornDamageCard(Chance activationChance, Power power) : base("Thorn card", activationChance)
    {
        Power = power;
    }
    public Power Power { get; }
}
