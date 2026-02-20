using Domain.Battles.Duels.DuelWarriorStates;
using Domain.Battles.Duels.Effects;
using Domain.Battles.Spheres;
using Domain.MagicCards;
using Domain.Shared;
using Domain.Warriors;

namespace Domain.Battles.Duels.ModEffectHandlers;

internal interface IModEffectHandler<in TModEffect> where TModEffect : EffectBase
{
    void Amplify(Modifiers mods, DuelWarriorState self, DuelWarriorState opponent, TModEffect effect);
}

internal interface IModEffectHandler
{
    Type EffectType { get; }
    void Amplify(Modifiers mods, DuelWarriorState self, DuelWarriorState opponent, EffectBase effect);
}

