
using Domain.Warriors;

namespace Domain.MagicCards;
public interface IMagicCardStrategy
{
    Type CardType { get; }

    void ApplyMagic(Warrior cardHolder, Warrior oponent, MagicCardBase card);
}

public interface IMagicCardStrategy<in TCard> where TCard : MagicCardBase
{
     void ApplyMagic(Warrior cardHolder, Warrior oponent, TCard card);
}
