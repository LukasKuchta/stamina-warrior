using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Domain.ActivationRules;
using Domain.BattlePlans;
using Domain.MagicCards;

namespace Domain.UnitTests.Battles.Shared;

internal sealed class SlotHelper
{
    public static Slot Create(MagicCardBase card)
    {
        return new Slot(card, new ChanceActivationRule(Chance.Always), 0);
    }

    public static Slot Create(MagicCardBase card, Chance chance)
    {
        return new Slot(card, new ChanceActivationRule(chance), 0);
    }

    public static Slot Create(MagicCardBase card, int priority, Func<AttackContext, bool> condition)
    {
        return new Slot(card, new ConditionActivationRule(condition), priority);
    }
}
