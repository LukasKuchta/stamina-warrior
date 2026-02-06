using Domain.MagicCards;
using Domain.RandomSources;

namespace Domain.UnitTests.Battles.Shared;

internal sealed class FakeIRandomSource : IRandomSource
{
    public int NextIntInclusive(int maxInclusive)
    {
        return maxInclusive;
    }

    public bool Succeeds(Chance chance)
    {
        return true;
    }
}
