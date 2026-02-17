using Domain.ActivationRules;
using Domain.RandomSources;

namespace Domain.UnitTests.Battles.Shared;

internal sealed class FakeIRandomSource(int index = 0, bool hitCHeck = true) : IRandomSource
{
    public int NextIntInclusive(int maxInclusive)
    {
        return index;
    }

    public bool Succeeds(Chance chance)
    {
        return hitCHeck;
    }
}

internal sealed class FalseRandomSource : IRandomSource
{
    public int NextIntInclusive(int maxInclusive)
    {
        return maxInclusive;
    }

    public bool Succeeds(Chance chance)
    {
        return false;
    }
}
