using System.Diagnostics.Contracts;
using Domain.MagicCards;
using Domain.MagicCards.Rules;
using Domain.Shared;

namespace Domain.BattlePlans;

internal sealed record BattlePlan : ValueObjectBase
{
    private BattlePlan(List<Slot> slots)
    {
        _slots = slots;
    }

    private readonly List<Slot> _slots;

    public int MaxIndexOfSlot => _slots.Count - 1;

    public int NumberOfsLOTS => _slots.Count;

    public bool Empty => _slots.Count == 0;

    public bool NotEmpty => !Empty;

    public bool Enchanted { get; private set; }

    public SlotResult TouchTheSlot(int slotIndfex)
    {
        CheckRule(new SlotIndexCannotBeNegativeRule(slotIndfex));

        if (slotIndfex <= MaxIndexOfSlot)
        {
            var slot = _slots[slotIndfex];
            _slots.RemoveAt(slotIndfex);

            return SlotResult.Create(slot);
        }

        return SlotResult.None;
    }

    public void Add(Slot slot)
    {
        _slots.Add(slot);
    }

    public static BattlePlan FromList(IEnumerable<Slot> slots)
    {
        return new BattlePlan(slots.ToList());
    }
}
