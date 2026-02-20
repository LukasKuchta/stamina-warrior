using Domain.Battles.Duels.DuelWarriorStates;
using Domain.Battles.Duels.Effects;

namespace Domain.Battles.Duels.StateEffectHandlers;

public interface IStateEffectHandler
{
    Type EffectType { get; }

    void ApplyEffect(DuelWarriorState self, DuelWarriorState opponent, EffectBase effect);
}

public interface IStateEffectHandler<in TEffect> where TEffect : EffectBase
{
    void Apply(DuelWarriorState self, DuelWarriorState opponent, TEffect effect);
}

