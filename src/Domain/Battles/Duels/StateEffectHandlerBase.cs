namespace Domain.Battles.Duels;

public abstract class StateEffectHandlerBase<TEffect> : IEffectHandler<TEffect>, IStateEffectHandler
    where TEffect : EffectBase
{
    public Type EffectType => typeof(TEffect);

    public void ApplyEffect(DuelWarriorState self, DuelWarriorState opponent, EffectBase effect)
    {
        Apply(self, opponent, (TEffect)effect);
    }

    public abstract void Apply(DuelWarriorState self, DuelWarriorState opponent, TEffect effect);
}

