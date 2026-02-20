using Domain.Battles.Duels.DuelWarriorStates;
using Domain.Battles.Duels.Effects;

namespace Domain.Battles.Duels.StateEffectHandlers;

public abstract class StateEffectHandlerBase<TEffect> : IStateEffectHandler<TEffect>, IStateEffectHandler
    where TEffect : EffectBase
{
    public Type EffectType => typeof(TEffect);

    public void ApplyEffect(DuelWarriorState self, DuelWarriorState opponent, EffectBase effect)
    {
        Apply(self, opponent, (TEffect)effect);
    }

    public abstract void Apply(DuelWarriorState self, DuelWarriorState opponent, TEffect effect);
}

