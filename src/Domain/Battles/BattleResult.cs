using System.Collections.Immutable;
using Domain.Battles.Events;
using Domain.Warriors;

namespace Domain.Battles;

public sealed class BattleResult
{
    private readonly ImmutableArray<IBattleEvent> _battleEvents;

    internal BattleResult(ImmutableArray<IBattleEvent> battleEvents)
    {
        _battleEvents = battleEvents;
    }

    public ImmutableArray<IBattleEvent> BattleEvents => _battleEvents;
}

