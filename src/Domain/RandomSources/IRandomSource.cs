using Domain.ActivationRules;

namespace Domain.RandomSources;

public interface IRandomSource
{
    bool Succeeds(Chance chance);

    int NextIntInclusive(int maxInclusive);
}
