using Domain.Battles.Spheres;

namespace Domain.Battles.Strategies;

public abstract class BattleStrategyBase<TSphereType> : IBattleStrategy<TSphereType>, IBattleStrategy
    where TSphereType : SphereBase
{
    public Type SphereType => typeof(TSphereType);

    public abstract BattleResult StartBattle(BattleContext battleContext, DateTimeOffset startedAt);
}
