using Domain.Battles.Duels;
using Domain.Shared;
using Domain.Warriors;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.Battles.Duels;

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


public readonly record struct Health
{
    public int Value { get; }

    private Health(int value) => Value = value;

    public static Health From(int value)
    {
        RuleChecker.CheckRule(new CannotBeNegativeRule(value, nameof(Health)));
        return new Health(value);
    }

    public Health Take(Damage dmg) => From(Math.Max(0, Value - dmg.Value));
    public Health Add(HealAmount heal) => From(Value + heal.Value);
}

public readonly record struct Damage
{
    public int Value { get; }

    private Damage(int value) => Value = value;

    public Damage Add(DamageAdd add) => From(Value + add.Value);

    public static Damage From(int value)
    {
        RuleChecker.CheckRule(new CannotBeNegativeRule(value, nameof(Damage)));
        return new Damage(value);
    }
}

public readonly record struct HealAmount
{
    public int Value { get; }

    private HealAmount(int value) => Value = value;

    public static HealAmount From(int value)
    {
        RuleChecker.CheckRule(new CannotBeNegativeRule(value, nameof(HealAmount)));
        return new HealAmount(value);
    }
}

public readonly record struct Accuracy
{
    public const int Min = 0;
    public const int Max = 100;

    public int Value { get; }
    private Accuracy(int value) => Value = value;

    public Accuracy Add(AccuracyAdd add) 
    {
        var next = Value + add.Value;
        next = Math.Clamp(next, Min, Max);
        return From(next);
    }    

    public static Accuracy From(int value)
    {
        RuleChecker.CheckRule(new CheckPercentageRangeRule(value));
        return new Accuracy(value);
    }
}

public readonly record struct Evasion
{
    public const int Min = 0;
    public const int Max = 100;

    public int Value { get; }
    private Evasion(int value) => Value = value;
    
    public Evasion Add(EvasionAdd add)
    {
        var next = Value + add.Value;
        next = Math.Clamp(next, Min, Max);
        return From(next);
    }

    public static Evasion From(int value)
    {
        RuleChecker.CheckRule(new CheckPercentageRangeRule(value));
        return new Evasion(value);
    }
}
