using Domain.Battles.Duels.Effects;
using Domain.Battles.Duels.Mods.AccuracyMods;
using Domain.Battles.Duels.Mods.DamageMods;
using Domain.Battles.Duels.Mods.EvasionMods;
using Domain.Shared;
using Domain.Warriors;

namespace Domain.Battles.Duels.DuelWarriorStates;

public sealed class DuelWarriorState : EntityBase, IAgregationRoot
{
    private DuelWarriorState(
        DuelId duelId,
        WarriorId warriorId,
        Health health,
        Damage baseDamae,
        Accuracy accuracy,
        Evasion evasion)
    {
        DuelId = duelId;
        WarriorId = warriorId;
        Health = health;
        BaseDamage = baseDamae;
        BaseAccuracy = accuracy;
        BaseEvasion = evasion;

        _readOnlyEffects = _effects.AsReadOnly();
    }

    private readonly List<EffectBase> _effects = [];    
    private readonly IReadOnlyCollection<EffectBase> _readOnlyEffects;

    public IReadOnlyCollection<EffectBase> Effects => _readOnlyEffects;
    public DuelId DuelId { get; }
    public WarriorId WarriorId { get; }
    public Health Health { get; private set; }
    public Damage BaseDamage { get; private set; }
    public Accuracy BaseAccuracy { get; private set; }
    public Evasion BaseEvasion { get; private set; }

    public void AdjustBaseDamagetat(DamageAdd add)
    {
        BaseDamage = BaseDamage.Add(add);
    }

    public void AdjustBaseAccuracyStat(AccuracyAdd add)
    {
        BaseAccuracy = BaseAccuracy.Add(add);
    }

    public void AdjustBaseEvasionStat(EvasionAdd add)
    {
        BaseEvasion = BaseEvasion.Add(add);
    }

    public void Hit(Damage damage)
    {
        Health = Health.Take(damage);
    }

    internal void Heal(HealAmount ammount)
    {
        Health = Health.Add(ammount);
    }

    internal void AddEffect(EffectBase effect)
    {
        _effects.Add(effect);
    }

    internal void RemoveEffect(EffectBase effect)
    {
        _effects.Remove(effect);
    }

    public static DuelWarriorState Create(
        DuelId duelId,
        WarriorId warriorId,
        Health health,
        Damage baseDamae,
        Accuracy accuracy,
        Evasion evasion)
    {
        return new DuelWarriorState(duelId, warriorId, health, baseDamae, accuracy, evasion);
    }
}
