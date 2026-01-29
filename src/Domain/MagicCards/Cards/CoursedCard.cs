namespace Domain.MagicCards.Cards;

public sealed record CoursedCard : MagicCardBase
{
    public CoursedCard(Chance chance, Power power) : base("Coursed card", chance)
    {
        Power = power;
    }

    public Power Power { get; }
}
