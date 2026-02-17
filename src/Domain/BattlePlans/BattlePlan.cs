using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using Domain.ActivationRules;
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

    public bool TryEvaluateRules(AttackContext attackContext, [NotNullWhen(true)] out Slot? slot)
    {        
        var s =  _slots
            .Where(slot => slot.Rule is ConditionActivationRule rule && rule.Condition(attackContext))
            .OrderByDescending(slot => slot.Priority)
            .FirstOrDefault();

        if (s is null)
        {
            slot = null;
            return false;
        }

        slot = s;
        return true;
    }

    public bool TouchTheSlot(int slotIndfex, [NotNullWhen(true)] out Slot? slot)    
    {
        CheckRule(new SlotIndexCannotBeNegativeRule(slotIndfex));

        if (slotIndfex <= MaxIndexOfSlot && _slots[slotIndfex].Rule is ChanceActivationRule)
        {
            slot = _slots[slotIndfex];
            _slots.RemoveAt(slotIndfex);

            return true;
        }

        slot = null;
        return false;
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

public sealed record ConditionMet(Slot? Slot, bool Met);
