using Domain.Warriors;

namespace Domain.Battles;

internal sealed class BattleStrategyFactory : IBattleStrategyFactory
{
    private readonly Dictionary<Sphere, IBattleStrategy> _strategies;

    public BattleStrategyFactory(IEnumerable<IBattleStrategy> strategies)
    {
        _strategies = strategies.ToDictionary(s => s.Sphere, s => s);
    }

    public IBattleStrategy SelectBy(Sphere sphere)
    {
        if (!_strategies.TryGetValue(sphere, out var strategy))
        {
            throw new ArgumentException($"No battle strategy found for sphere: {sphere}");
        }

        return strategy;
    }
}
