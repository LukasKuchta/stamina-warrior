namespace Domain.MagicCards.Cards;

public sealed record StealingCard : MagicCardBase
{
    internal StealingCard(Chance activationChance) : base("Stealing card", activationChance)
    {
    } 
}
