using Domain.Battles.Duels.AttackExchanges;
using Domain.Shared;

namespace Domain.Battles.Duels.Rules;

public sealed class AllowOnlyTransitionDraftToTimeoutRule(AttackExchangeState state) : IBusinessRule
{
    public string Message => $"Attack exchange allows transition from draft to timeout state. current: {state}";

    public bool IsBroken()
    {
        return state != AttackExchangeState.Draft;
    }
}
