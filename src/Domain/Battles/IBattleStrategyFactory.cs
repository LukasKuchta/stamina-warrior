
using Domain.Warriors;

namespace Domain.Battles;
public interface IBattleStrategyFactory
{
    IBattleStrategy SelectBy(Sphere sphere);
}
