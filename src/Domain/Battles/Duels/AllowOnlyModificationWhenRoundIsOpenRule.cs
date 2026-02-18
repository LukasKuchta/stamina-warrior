using Domain.Shared;

namespace Domain.Battles.Duels;

public sealed class AllowOnlyModificationWhenRoundIsOpenRule(DuelRoundState state) : IBusinessRule
{
    public string Message => "Modifications are only allowed when the round is open.";

    public bool IsBroken()
    {
        return state != DuelRoundState.Open;
    }
}
