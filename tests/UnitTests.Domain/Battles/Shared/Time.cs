using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.UnitTests.Battles.Shared;

internal static class Time
{
    public static DateTimeOffset StartBattleAt => new DateTimeOffset(1985, 2, 16, 8, 45, 10, TimeSpan.Zero);
}
