using Domain.Warriors;

namespace Domain.Battles.Events;


public sealed record CardDrawn : BattleEventBase
{
    internal CardDrawn(Warrior cardHolder, string cardName)
    {
        CardHolder = cardHolder.Name;
        CardName = cardName;
    }

    public string CardHolder { get; }
    public string CardName { get; }

    public override void Accept(IBattleEventVisitor visitor) => visitor.Visit(this);
}
