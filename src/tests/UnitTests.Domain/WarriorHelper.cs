using System;
using System.Collections.Generic;
using System.Text;
using Domain.MagicCards;
using Domain.Warriors;

namespace Domain.UnitTests;

internal sealed class WarriorHelper
{
    public static Warrior CreateBlueSkyWarrior(string name, int level, IEnumerable<MagicCardBase>? cards = null)
    {
        IEnumerable<MagicCardBase> magicCards = cards is null ? Enumerable.Empty<MagicCardBase>() : cards;

        WarriorId warriorId = WarriorId.New();
        return Warrior.Create(
            warriorId,
            name,
            Sphere.BlueSky,
            Level.FromNumber(level),
            magicCards);
    }
}
