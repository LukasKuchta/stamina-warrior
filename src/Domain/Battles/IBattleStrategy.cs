using Domain.Battles.Spheres;

namespace Domain.Battles;


public interface IBattleStrategy
{
    BattleResult StartBattle(BattleContext battleContext, DateTimeOffset startedAt);
}


