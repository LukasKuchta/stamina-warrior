namespace Domain.MagicCards.Cards;

public sealed record FightingCard : MagicCardBase
{
    public FightingCard(Chance chance, Power power) : base("Fighting card", chance)
    {
        Power = power;
    }

    public Power Power { get; }
}

