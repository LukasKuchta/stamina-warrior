using Domain.MagicCards.Cards;
using Domain.Warriors;

namespace Domain.MagicCards.Strategies;

public sealed class FightingCardStrategy : MagicCardStrategyBase<FightingCard>
{
    public override void ApplyMagic(Warrior cardHolder, Warrior oponent, FightingCard card)
    {
        cardHolder.BoostDamage(card.Power);
    }
}
