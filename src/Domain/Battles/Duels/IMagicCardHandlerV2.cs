using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Domain.Battles.Spheres;
using Domain.MagicCards;
using Domain.Warriors;

namespace Domain.Battles.Duels;

internal interface IMagicCardHandlerV2
{
    Type CardType { get; }
    void Apply(DuelWarriorState self, DuelWarriorState opponent, MagicCardBase card);
}

internal interface IMagicCardHandlerV2<in TCard> where TCard : MagicCardBase
{  
    void Apply(DuelWarriorState self, DuelWarriorState opponent, TCard card);
}
