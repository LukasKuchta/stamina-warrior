using Domain.Shared;

namespace Domain.Battles.Duels;

public sealed class OnlyCreatedStateCanBeCHangedRule(AttackExchangeState state) : IBusinessRule
{
    public string Message => "Only attack exchanges in 'Created' state can be changed.";

    public bool IsBroken()
    {
        return state != AttackExchangeState.Draft;
    }
}
