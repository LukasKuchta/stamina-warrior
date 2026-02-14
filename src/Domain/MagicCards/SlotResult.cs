using Domain.BattlePlans;

namespace Domain.MagicCards;

public sealed record SlotResult
{
    public static SlotResult None => new SlotResult();

    public Slot? Slot { get; init; }

    private SlotResult()
    {
    }

    private SlotResult(Slot slot)
    {
        Slot = slot;
    }

    public static SlotResult Create(Slot slot)
    {
        return new SlotResult(slot);
    }
}
