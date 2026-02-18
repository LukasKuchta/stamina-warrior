using Domain.Shared;

namespace Domain.Battles.Duels.Rules;

public sealed class AllowOnlyTransitionDraftToReadyRule(AttackExchangeState state) : IBusinessRule
{
    public string Message => $"Attack exchange allows transition from {AttackExchangeState.Draft} to {AttackExchangeState.Ready} state. current: {state}";

    public bool IsBroken()
    {
        return state != AttackExchangeState.Draft;
    }
}
