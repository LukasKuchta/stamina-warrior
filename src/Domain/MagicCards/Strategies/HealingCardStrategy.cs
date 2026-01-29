using Domain.MagicCards.Cards;
using Domain.Warriors;

namespace Domain.MagicCards.Strategies;

public sealed class HealingCardStrategy : MagicCardStrategyBase<HealingCard>
{
    public override void ApplyMagic(Warrior cardHolder, Warrior oponent, HealingCard card)
    {
        cardHolder.Heal(card.Power);
    }
}
