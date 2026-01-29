namespace Domain.MagicCards.Cards;

public sealed record HealingCard : MagicCardBase
{
    public HealingCard(Chance activationChance, Power power) : base("Card of healing", activationChance)
    {
        Power = power;
    }

    public Power Power { get; }
}
