namespace Domain.Battles.Duels;

public interface IEffectHandler<in TEffect> where TEffect : EffectBase
{
    void Apply(DuelWarriorState self, DuelWarriorState opponent, TEffect effect);
}
