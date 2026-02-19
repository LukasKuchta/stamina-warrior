using Domain.Battles.Duels;
using Domain.Shared;
using Domain.Warriors;

namespace Domain.Battles.Duels;

public sealed class DuelWarriorState : EntityBase, IAgregationRoot
{
    private DuelWarriorState(DuelId duelId, WarriorId warriorId, int baseDamage)
    {
        DuelId = duelId;
        WarriorId = warriorId;
        BaseDamage = baseDamage;

        _readOnlyEffects = _effects.AsReadOnly();
    }

    private readonly List<EffectBase> _effects = [];
    public IReadOnlyCollection<EffectBase> Effects => _readOnlyEffects;
    private readonly IReadOnlyCollection<EffectBase> _readOnlyEffects;

    public void Heal(int heal)
    {
        Health += heal;
    }

    internal void AddEffect(EffectBase effect)
    {
        _effects.Add(effect);
    }



    public int Damage()
    {
        return BaseDamage;
    }

    public int BaseDamage { get; }
    public DuelId DuelId { get; }
    public WarriorId WarriorId { get; }
    public int Health { get; private set; }
    public int Armor { get; private set; }
    public int Accuracy { get; private set; }
    public int Evasion { get; private set; }

    public void Hit(int damage)
    {
        Health -= damage;
    }

    public static DuelWarriorState Create(DuelId duelId, WarriorId warriorId, int baseDamage)
    {
        return new DuelWarriorState(duelId, warriorId, baseDamage);
    }

}
