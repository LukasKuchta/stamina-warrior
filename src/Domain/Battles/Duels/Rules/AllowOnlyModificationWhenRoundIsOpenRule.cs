using Domain.Battles.Duels.Rounds;
using Domain.Shared;

namespace Domain.Battles.Duels.Rules;

public sealed class AllowOnlyModificationWhenRoundIsOpenRule(DuelRoundState state) : IBusinessRule
{
    public string Message => "Modifications are only allowed when the round is open.";

    public bool IsBroken()
    {
        return state != DuelRoundState.Open;
    }
}
