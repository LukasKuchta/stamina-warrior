namespace Domain.Battles.Duels;

internal abstract class ModEffectHandlerBase<TModEffect> : IModEffectHandler<TModEffect>, IModEffectHandler
    where TModEffect : EffectBase
{
    public Type EffectType => typeof(TModEffect);

    public void Amplify(Modifiers mods, DuelWarriorState self, DuelWarriorState opponent, EffectBase effect) => Amplify(mods, self, opponent, (TModEffect)effect);

    public abstract void Amplify(Modifiers mods, DuelWarriorState self, DuelWarriorState opponent, TModEffect effect);
}

