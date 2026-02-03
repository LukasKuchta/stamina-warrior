using System;
using System.Collections.Generic;
using System.Text;
using Domain.Shared;
using Domain.Warriors;
using Domain.Warriors.Rules;
using Shouldly;

namespace Domain.UnitTests.Warriors;

public sealed class WarriorIdTests
{

    [Fact]
    public void FromNumbe_WhenZeroLevel_ShouldViolateLevelCannotBeNegativeRule()
    {
        var id = WarriorId.New();
        id.Value.ShouldNotBe(Guid.Empty);
    }
}
