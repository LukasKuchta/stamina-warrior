namespace Domain.Battles.Duels;

public interface IStateEffectHandler
{
    Type EffectType { get; }

    void Handle(DuelWarriorState self, DuelWarriorState opponent, EffectBase effect);
}
