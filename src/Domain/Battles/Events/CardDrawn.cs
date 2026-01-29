using Domain.Warriors;

namespace Domain.Battles.Events;


public sealed record CardDrawn : IBattleEvent
{
    internal CardDrawn(Warrior cardHolder, string cardName) 
    { 
        CardHolder = cardHolder.Name;
        CardName = cardName;
    }

    public string CardHolder { get; }
    public string CardName { get; }

    public void Accept(IBattleEventVisitor visitor) => visitor.Visit(this);
}
