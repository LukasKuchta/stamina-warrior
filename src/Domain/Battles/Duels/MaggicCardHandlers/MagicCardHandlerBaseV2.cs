using Domain.Battles.Duels.DuelWarriorStates;
using Domain.MagicCards;

namespace Domain.Battles.Duels.MaggicCardHandlers;

internal abstract class MagicCardHandlerBaseV2<TCard> : IMagicCardHandlerV2<TCard>, IMagicCardHandlerV2 where TCard : MagicCardBase
{
    public Type CardType => typeof(TCard);

    void IMagicCardHandlerV2.Apply(DuelWarriorState self, DuelWarriorState opponent, MagicCardBase card) => Apply(self, opponent, (TCard)card);

    public abstract void Apply(DuelWarriorState self, DuelWarriorState opponent, TCard card);
}
