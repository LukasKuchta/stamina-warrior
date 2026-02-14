using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Domain.Battles.Events;
using Domain.Shared;

namespace Domain.Battles.Rules;

public sealed class BattleEventsCannotBeEmptyRule : IBusinessRule
{
    private readonly ImmutableArray<IBattleEvent> _battleEvents;

    internal BattleEventsCannotBeEmptyRule(ImmutableArray<IBattleEvent> battleEvents)
    {
        _battleEvents = battleEvents;
    }

    public string Message => "BattleEvents cannot be empty!";

    public bool IsBroken()
    {
        return _battleEvents.IsEmpty;
    }
}
