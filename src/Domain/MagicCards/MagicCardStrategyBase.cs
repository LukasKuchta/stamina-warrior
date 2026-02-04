using Domain.Warriors;

namespace Domain.MagicCards;

public abstract class MagicCardStrategyBase<TCard> : IMagicCardStrategy<TCard>, IMagicCardStrategy
    where TCard : MagicCardBase
{
    public Type CardType => typeof(TCard);

    public void ApplyMagic(Warrior cardHolder, Warrior oponent, MagicCardBase card)
    {        
        ApplyMagic(cardHolder, oponent, (TCard)card);
    }

    public abstract void ApplyMagic(Warrior cardHolder, Warrior oponent, TCard card); 
}
