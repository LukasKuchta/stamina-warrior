using Domain.Battles.Spheres;
using Domain.MagicCards;
using Domain.Shared;
using Domain.Warriors;

namespace Domain.Battles.Duels;

internal interface IModEffectHandler<in TModEffect> where TModEffect : EffectBase
{
    void Apply(Modifiers mods, DuelWarriorState self, DuelWarriorState opponent, TModEffect effect);
}

internal interface IModEffectHandler
{
    Type EffectType { get; }
    void Handle(Modifiers mods, DuelWarriorState self, DuelWarriorState opponent, EffectBase effect);
}

