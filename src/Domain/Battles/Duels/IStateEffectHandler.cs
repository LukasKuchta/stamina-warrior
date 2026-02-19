namespace Domain.Battles.Duels;

public interface IStateEffectHandler
{
    Type EffectType { get; }

    void Apply(DuelWarriorState self, DuelWarriorState opponent, EffectBase effect);
}
