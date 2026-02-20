using System.ComponentModel;
using System.Threading.Channels;
using Domain.ActivationRules;
using Domain.Battles.Duels;
using Domain.RandomSources;
using Domain.Warriors;

namespace Domain.Battles;

public sealed class FightDecisionSource(IRandomSource chanceService) : IFightDecisionSource
{
    public int PickDamage(int maxDamage)
    {
        return chanceService.NextIntInclusive(maxDamage);
    }

    public int PickSlotIndex(int maxCardIndex)
    {
        return chanceService.NextIntInclusive(maxCardIndex);
    }

    public bool HitCheck(Warrior attacker)
    {
        float @base = 0.75f;
        float scale = 0.01f;
        float chance = @base + (attacker.Accuracy - attacker.Evasion) * scale;

        return chanceService.Succeeds(Chance.FromValue(chance));
    }
}


public interface IHitCheck
{
    bool Attempt(Accuracy selfAccuracy, DuelWarriorState opponent);
}
public sealed class HitCheck(IRandomSource chanceService) : IHitCheck
{
    private const float _baseChance = 0.75f;
    private const float _scale = 0.01f;
    private const float _minChance = 0.05f;
    private const float _maxChance = 0.95f;

    public bool Attempt(Accuracy selfAccuracy, DuelWarriorState opponent)
    {
        int diff = selfAccuracy.Value - opponent.BaseEvasion.Value;
        float chance = Math.Clamp(_baseChance + diff * _scale, _minChance, _maxChance);
        return chanceService.Succeeds(Chance.FromValue(chance));
    }
}
