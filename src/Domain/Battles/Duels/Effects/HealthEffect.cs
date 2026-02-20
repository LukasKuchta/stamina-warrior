using Domain.Battles.Duels.DuelWarriorStates;
using Domain.Battles.Duels.Rules;
using Domain.Shared;

namespace Domain.Battles.Duels.Effects;

public sealed record HealthEffect : EffectBase
{
    public HealAmount Amount { get; }
    private int _remainingUses;

    public bool IsConsumed => _remainingUses == 0;
    private HealthEffect(HealAmount ammount, int retryCount)
    {
        Amount = ammount;
        _remainingUses = retryCount;
    }

    public bool Consume()
    {
        if (_remainingUses == 0)
        {
            return false;
        }

        --_remainingUses;
        return true;
    }

    public static HealthEffect Create(HealAmount ammount, int uses)
    {        
        RuleChecker.CheckRule(new MustBePositive(uses, nameof(uses)));
        
        return new HealthEffect(ammount, uses);
    }
}


