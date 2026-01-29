using System;
using Domain.Battles.Events;
using Domain.Warriors;

namespace Domain.Battles;

public sealed record BattleResult 
{
    private readonly IBattleEvent[] _battleEvents;
    internal BattleResult(IBattleEvent[] battleEvents)
    {
        _battleEvents = battleEvents;        
    }

    public ReadOnlySpan<IBattleEvent> BattleEvents => _battleEvents;
}

