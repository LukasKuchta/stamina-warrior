using Domain.ActivationRules;

namespace Domain.MagicCards.Cards;

public sealed record StealingCard : MagicCardBase
{
    public StealingCard() : base("Stealing card")
    {
    }
}
