namespace Domain.Battles.Duels;

internal abstract class ModEffectHandlerBase<TModEffect> : IModEffectHandler<TModEffect>, IModEffectHandler
    where TModEffect : EffectBase
{
    public Type EffectType => typeof(TModEffect);

    public void Handle(Modifiers mods, DuelWarriorState self, DuelWarriorState opponent, EffectBase effect) => Apply(mods, self, opponent, (TModEffect)effect);

    public abstract void Apply(Modifiers mods, DuelWarriorState self, DuelWarriorState opponent, TModEffect effect);
}

