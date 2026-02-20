namespace Domain.Battles.Duels;

public interface IStateEffectHandler
{
    Type EffectType { get; }

    void ApplyEffect(DuelWarriorState self, DuelWarriorState opponent, EffectBase effect);
}
