using Domain.BattlePlans;
using Domain.Battles.Spheres;
using Domain.MagicCards;
using Domain.Warriors;

namespace Domain.UnitTests;

internal sealed class WarriorHelper
{
    public static Warrior CreateBlueSky(string name, int level = 1, IEnumerable<Slot>? cards = null)
    {
        return Create(name, level, SphereBase.BlueSky, cards);
    }

    private static Warrior Create(string name, int level, SphereBase sphere, IEnumerable<Slot>? slots = null)
    {
        IEnumerable<Slot> battlePlan = slots is null ? Enumerable.Empty<Slot>() : slots;

        WarriorId warriorId = WarriorId.New();
        return Warrior.Create(
            warriorId,
            name,
            sphere,
            Level.FromNumber(level),
            battlePlan);
    }

    public static Warrior CreateBetweenworld(string name, int level = 1, IEnumerable<Slot>? cards = null)
    {
        return Create(name, level, SphereBase.Betweenworld, cards);
    }

    public static Warrior CreateDeepvault(string name, int level = 1, IEnumerable<Slot>? cards = null)
    {
        return Create(name, level, SphereBase.Deepvault, cards);
    }
}
