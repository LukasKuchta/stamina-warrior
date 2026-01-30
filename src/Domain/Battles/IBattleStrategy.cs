using Domain.Battles.Spheres;

namespace Domain.Battles;


public interface IBattleStrategy
{
    Type SphereType { get; }
    BattleResult StartBattle(BattleContext battleContext);
}

public interface IBattleStrategy<T> where T : SphereBase
{     
}
