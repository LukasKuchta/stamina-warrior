using Domain.Warriors;

namespace Domain.Battles;
public interface IBattleStrategy
{
    Sphere Sphere { get; }

    BattleResult StartBattle(BattleContext battleContext);
}
