using Domain.MagicCards.Cards;
using Domain.RandomSources;
using Domain.Warriors;

namespace Domain.MagicCards.Strategies;

public sealed class StealingCardStrategy(IRandomSource randomSource) : MagicCardStrategyBase<StealingCard>
{
    public override void ApplyMagic(Warrior cardHolder, Warrior oponent, StealingCard card)
    {
        if (!oponent.IsBattlePlanEmpty)
        {
            int cardIndex = randomSource.NextIntInclusive(oponent.BattlePlanMaxIndexOfSlotInclusive);
            cardHolder.StealCard(cardIndex, oponent);
        }
    }
}
