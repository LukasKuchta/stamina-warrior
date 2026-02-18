using Domain.Shared;

namespace Domain.Battles.Duels.Rules;

public sealed class AllowOnlyTransitionReadyToResolvedRule(AttackExchangeState state) : IBusinessRule
{
    public string Message => $"Attack exchange allows transition from pending to resolved state. current: {state}";

    public bool IsBroken()
    {
        return state != AttackExchangeState.Ready;
    }
}
