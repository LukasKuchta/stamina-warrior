using System;
using Domain.Shared;

namespace Domain.Battles.Duels;


internal sealed record AccuracyMod(AccuracyAdd Add) : ModBase
{
    public static readonly AccuracyMod None = new(AccuracyAdd.None);

    public Accuracy Apply(Accuracy @base)
    {
        return @base.Add(Add);
    }
}

internal sealed record EvasionMod(EvasionAdd Add) : ModBase
{
    public static readonly EvasionMod None = new(EvasionAdd.None);

    public Evasion Apply(Evasion @base)
    {
        return @base.Add(Add);
    }
}

internal sealed record DamageMod(DamageAdd Add, DamageMul Mul) : ModBase
{
    public static readonly DamageMod None = new(DamageAdd.None, DamageMul.None);

    public Damage Apply(Damage baseDamage)
    {
        var afterAdd = baseDamage.Value + Add.Value;
        if (afterAdd < 0)
        {
            afterAdd = 0;
        }

        var afterMul = Mul.ApplyTo(afterAdd);
        if (afterMul < 0)
        {
            afterMul = 0;
        }

        return Damage.From(afterMul);
    }
}

public readonly record struct DamageMul
{
    private const int @base = 10_000;
    public static readonly DamageMul None = From(@base); // 1.0x

    public int BasisPoints { get; }

    private DamageMul(int basisPoints)
    {
        BasisPoints = basisPoints;
    }

    public static DamageMul From(int basisPoints)
    {
        RuleChecker.CheckRule(new CannotBeNegativeRule(basisPoints, nameof(basisPoints)));

        return new DamageMul(basisPoints);
    }

    public int ApplyTo(int value)
        => value * BasisPoints / @base;

    public static DamageMul FromBonusPercent(int percent)
        => From(@base + percent * 100);
}

public readonly record struct DamageAdd(int Value)
{
    public static readonly DamageAdd None = new(0);
}
