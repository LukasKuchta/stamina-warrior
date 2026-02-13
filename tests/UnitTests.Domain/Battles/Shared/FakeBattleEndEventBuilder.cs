using Domain.Battles;
using Domain.Battles.Events;
using Domain.Battles.Strategies;

namespace Domain.UnitTests.Battles.Shared;

internal sealed class FakeBattleEndEventBuilder : IBattleEndEventBuilder
{
    public IBattleEvent? TryBuildEndEvent(BattleContext ctx, bool isLastRound) => null;
}
