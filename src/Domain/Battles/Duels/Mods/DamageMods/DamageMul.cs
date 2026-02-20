using Domain.Battles.Duels.Rules;
using Domain.Shared;

namespace Domain.Battles.Duels.Mods.DamageMods;

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
