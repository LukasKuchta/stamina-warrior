using Domain.ActivationRules;
using Domain.RandomSources;

namespace Domain.UnitTests.Battles.Shared;

internal sealed class FakeIRandomSource(int index = 0) : IRandomSource
{
    public int NextIntInclusive(int maxInclusive)
    {
        return index;
    }

    public bool Succeeds(Chance chance)
    {
        return true;
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
