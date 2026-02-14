using Domain.ActivationRules;
using Domain.MagicCards;

namespace Domain.BattlePlans;

public sealed record Slot(MagicCardBase Card, ActivationRuleBase Rule, int Priority);
