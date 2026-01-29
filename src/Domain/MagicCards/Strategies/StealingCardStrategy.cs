using Domain.MagicCards.Cards;
using Domain.RandomSources;
using Domain.Warriors;

namespace Domain.MagicCards.Strategies;

public sealed class StealingCardStrategy(IRandomSource randomSource) : MagicCardStrategyBase<StealingCard>
{
    public override void ApplyMagic(Warrior cardHolder, Warrior oponent, StealingCard card)
    {
        if (oponent.IsDeckOfCardsEmpty)
        {
            int cardIndex = randomSource.NextIntInclusive(oponent.DeckMaxIndexInclusive);
            cardHolder.StealCard(cardIndex, oponent);
        }
    }
}
