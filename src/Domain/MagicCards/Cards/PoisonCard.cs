namespace Domain.MagicCards.Cards;

public sealed record PoisonCard : MagicCardBase
{
    public PoisonCard(Chance activationChance) : base("Poisoned card", activationChance)
    {
    }

    public Power Power { get; }
}
