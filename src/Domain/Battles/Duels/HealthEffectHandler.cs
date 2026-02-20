namespace Domain.Battles.Duels;

public sealed class HealthEffectHandler : StateEffectHandlerBase<HealthEffect>
{
    public override void Apply(DuelWarriorState self, DuelWarriorState opponent, HealthEffect effect)
    {
        self.Heal(effect.Amount);
        _ = effect.Consume();
    }
}

