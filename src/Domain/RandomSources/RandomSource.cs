using System.Security.Cryptography;
using Domain.ActivationRules;

namespace Domain.RandomSources;

public sealed class RandomSource : IRandomSource
{
    private static readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();
    private readonly Func<double> _nextDouble;

    public RandomSource() : this(NextDoubleCrypto) { }

    public RandomSource(Func<double> nextDouble)
        => _nextDouble = nextDouble;

    public bool Succeeds(Chance chance)
    {
        if (chance.IsNever())
        {
            return false;
        }

        if (chance.IsAlways())
        {
            return true;
        }

        return _nextDouble() < chance.Value;
    }

    public int NextIntInclusive(int maxInclusive)
    {
        if (maxInclusive < 0)
        {
            throw new ArgumentException("maxInclusive must be greater or equal zero.\"", nameof(maxInclusive));
        }

        if (maxInclusive == 0)
        {
            return 0;
        }

        var bytes = new byte[4];
        _rng.GetBytes(bytes);
        int value = BitConverter.ToInt32(bytes, 0) & int.MaxValue;
        return value % maxInclusive;
    }

    private static double NextDoubleCrypto()
    {
        Span<byte> bytes = stackalloc byte[8];
        RandomNumberGenerator.Fill(bytes);               // crypto random bytes :contentReference[oaicite:2]{index=2}

        ulong ul = BitConverter.ToUInt64(bytes);
        // Stryker disable once Assignment: equivalent mutant
        ul >>= 11;                                       // 53 bits  mantisa
        return ul * (1.0 / (1UL << 53));                 // [0, 1)
    }
}
