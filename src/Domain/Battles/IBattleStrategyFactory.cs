
using Domain.Battles.Spheres;

namespace Domain.Battles;

public interface IBattleStrategyFactory
{
    IBattleStrategy SelectBy(SphereBase sphere);
}
