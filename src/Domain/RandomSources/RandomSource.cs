using Domain.MagicCards;
using System;
using System.Security.Cryptography;

namespace Domain.RandomSources;

internal sealed class RandomSource : IRandomSource
{
    private static readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();

    public bool Succeeds(Chance chance)
    {
        if (chance.IsNone())
        {
            return false;
        }

        if (chance.IsAlways())
        {
            return true;
        }

        return NextDoubleCrypto() < chance.Value;
    }

    public int NextIntInclusive(int maxInclusive)
    {
        if (maxInclusive < 0)
        {
            throw new ArgumentException("maxInclusive must be greater than zero.", nameof(maxInclusive));
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
        ul >>= 11;                                       // 53 bits  mantisa
        return ul * (1.0 / (1UL << 53));                 // [0, 1)
    }
}
