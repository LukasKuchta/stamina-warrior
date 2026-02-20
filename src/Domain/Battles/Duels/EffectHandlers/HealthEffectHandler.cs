using Domain.Battles.Duels.DuelWarriorStates;
using Domain.Battles.Duels.Effects;
using Domain.Battles.Duels.StateEffectHandlers;

namespace Domain.Battles.Duels.EffectHandlers;

public sealed class HealthEffectHandler : StateEffectHandlerBase<HealthEffect>
{
    public override void Apply(DuelWarriorState self, DuelWarriorState opponent, HealthEffect effect)
    {
        self.Heal(effect.Amount);
        _ = effect.Consume();
    }
}

