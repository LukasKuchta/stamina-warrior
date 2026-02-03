using System;
using System.Collections.Generic;
using System.Text;
using Domain.Battles;
using Domain.Battles.Spheres;
using Domain.Battles.Strategies;
using Domain.MagicCards;
using Newtonsoft.Json.Serialization;

namespace Domain.UnitTests.Battles.Shared;

internal sealed class BattleStrategyHelper
{
    public static IBattleStrategy CreateDefaultBlueSky()
    {
        return new BlueSkyBattleStrategy(
             new MagicCardStrategyFactory([]),
             new EchoDecisionSource(0),
             new BattleEndEventBuilder());
    }
}
