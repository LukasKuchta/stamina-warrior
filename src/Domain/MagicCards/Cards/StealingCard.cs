namespace Domain.MagicCards.Cards;

public sealed record StealingCard : MagicCardBase
{
    public StealingCard(Chance activationChance) : base("Stealing card", activationChance)
    {
    } 
}
