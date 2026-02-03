using System;
using System.Collections.Generic;
using System.Text;
using Domain.Battles.Spheres;
using Domain.MagicCards;
using Domain.Warriors;

namespace Domain.UnitTests;

internal sealed class WarriorHelper
{
    public static Warrior CreateBlueSky(string name, int level = 1, IEnumerable<MagicCardBase>? cards = null)
    {
        return Create(name, level, SphereBase.BlueSky, cards);
    }

    private static Warrior Create(string name, int level, SphereBase sphere, IEnumerable<MagicCardBase>? cards = null)
    {
        IEnumerable<MagicCardBase> magicCards = cards is null ? Enumerable.Empty<MagicCardBase>() : cards;

        WarriorId warriorId = WarriorId.New();
        return Warrior.Create(
            warriorId,
            name,
            sphere,
            Level.FromNumber(level),
            magicCards);
    }

    public static Warrior CreateBetweenworld(string name, int level = 1, IEnumerable<MagicCardBase>? cards = null)
    {
        return Create(name, level, SphereBase.Betweenworld, cards);
    }

    public static Warrior CreateDeepvault(string name, int level = 1, IEnumerable<MagicCardBase>? cards = null)
    {
        return Create(name, level, SphereBase.Deepvault, cards);
    }
}
