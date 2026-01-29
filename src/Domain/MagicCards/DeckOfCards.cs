
using Domain.MagicCards.Cards;
using Domain.MagicCards.Rules;
using Domain.Shared;
using System.Collections.Generic;

namespace Domain.MagicCards;
internal sealed record DeckOfCards : ValueObjectBase
{
    private DeckOfCards(List<MagicCardBase> cards)
    {
        _cards = cards;
    }

    private readonly List<MagicCardBase> _cards;

    public int MaxIndexOfCard => _cards.Count - 1;
    
    public int NumberOfCards => _cards.Count;

    public bool Empty => _cards.Count == 0;

    public bool NotEmpty => !Empty;

    public bool Enchanted { get; private set; }

    public DrawResult DrawACard(int luckyNumber)
    {
        CheckRule(new CardIndexCannotBeNegativeRule(luckyNumber));

        if (Empty)
        {
            return DrawResult.None;
        }

        if (luckyNumber <= MaxIndexOfCard)
        {
            var luckyCard = _cards[luckyNumber];
            _cards.RemoveAt(luckyNumber);

            return DrawResult.Create(luckyCard);
        }

        return DrawResult.None;
    }

    public void Add(MagicCardBase card)
    {
        ArgumentNullException.ThrowIfNull(card);

        _cards.Add(card);
    }

    public DrawResult TryToDrawPoisonedCard()
    {
        var poisonedCard = _cards.FirstOrDefault(card => card is PoisonCard);
        if (poisonedCard is not null)
        {
            return DrawResult.Create(poisonedCard);
        }

        return DrawResult.None;
    }

    public static DeckOfCards Creeate()
    {
        return new DeckOfCards(new List<MagicCardBase>());
    }

    public static DeckOfCards FromList(IList<MagicCardBase> cards)
    {
        return new DeckOfCards(cards.ToList<MagicCardBase>());
    }
}
