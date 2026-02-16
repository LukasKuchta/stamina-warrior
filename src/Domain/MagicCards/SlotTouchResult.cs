using Domain.BattlePlans;

namespace Domain.MagicCards;

public sealed record SlotTouchResult
{
    public static SlotTouchResult None => new SlotTouchResult();

    public Slot? Slot { get; init; }

    private SlotTouchResult()
    {
    }

    private SlotTouchResult(Slot slot)
    {
        Slot = slot;
    }

    public static SlotTouchResult Create(Slot slot)
    {
        return new SlotTouchResult(slot);
    }
}

internal sealed record BattlePlanConditionMet(Slot Slot);
