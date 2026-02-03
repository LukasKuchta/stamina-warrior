using Domain.Battles.Spheres;
using Domain.MagicCards;

namespace Domain.Battles;

public sealed class BattleStrategyFactory : IBattleStrategyFactory
{
    private readonly Dictionary<Type, IBattleStrategy> _battleStrategies;

    public BattleStrategyFactory(IEnumerable<IBattleStrategy> strategies) => _battleStrategies = strategies.ToDictionary(s => s.SphereType);

    public IBattleStrategy SelectBy(SphereBase sphere)
    {        
        if (!_battleStrategies.TryGetValue(sphere.GetType(), out var strategy))
        {
            throw new ArgumentException($"No battle strategy found for sphere: {sphere}");
        }

        return strategy;
    }
}
