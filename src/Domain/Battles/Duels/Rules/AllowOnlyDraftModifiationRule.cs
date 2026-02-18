using Domain.Shared;

namespace Domain.Battles.Duels.Rules;

public sealed class AllowOnlyDraftModifiationRule(AttackExchangeState state) : IBusinessRule
{
    public string Message => "Only attack exchanges in Draft state can be modified.";

    public bool IsBroken()
    {
        return state != AttackExchangeState.Draft;
    }
}
