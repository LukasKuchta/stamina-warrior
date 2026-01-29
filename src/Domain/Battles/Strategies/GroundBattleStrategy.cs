using Domain.Warriors;

namespace Domain.Battles.Strategies;

internal sealed class GroundBattleStrategy : IBattleStrategy
{
    public Sphere Sphere => Sphere.Betweenworld;

    public BattleResult StartBattle(BattleContext battleContext)
    {
        throw new NotImplementedException();
    }
}
