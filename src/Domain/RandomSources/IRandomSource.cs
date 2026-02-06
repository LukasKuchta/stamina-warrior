using Domain.MagicCards;

namespace Domain.RandomSources;

public interface IRandomSource
{
    bool Succeeds(Chance chance);

    int NextIntInclusive(int maxInclusive);
}
