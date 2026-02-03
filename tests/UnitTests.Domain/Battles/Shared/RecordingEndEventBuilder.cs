using System;
using System.Collections.Generic;
using System.Text;
using Domain.Battles;
using Domain.Battles.Events;
using Domain.Battles.Strategies;

namespace Domain.UnitTests.Battles.Shared;

internal sealed class RecordingEndEventBuilder : IBattleEndEventBuilder
{
    public List<bool> Flags { get; } = new();    

    public IBattleEvent? TryBuildEndEvent(BattleContext ctx, bool isLastRound)
    {
        Flags.Add(isLastRound);
        return null;
    }
}
