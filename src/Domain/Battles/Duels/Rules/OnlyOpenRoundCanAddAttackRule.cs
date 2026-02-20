using Domain.Battles.Duels.Rounds;
using Domain.Shared;

namespace Domain.Battles.Duels.Rules;

public sealed class OnlyOpenRoundCanAddAttackRule(DuelRoundState state) : IBusinessRule
{
    public string Message => "Only open round can add attack exchange.";

    public bool IsBroken()
    {
        return state != DuelRoundState.Open;
    }
}
