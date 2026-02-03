
using Domain.Shared;
using Domain.Warriors;

namespace Domain.Battles.Rules;
public sealed record AttackerAndOpponentSpehereCheckRule : IBusinessRule
{
    private readonly Warrior _attacker;
    private readonly Warrior _opponent;

    internal AttackerAndOpponentSpehereCheckRule(Warrior attacker, Warrior opponent)
    {
        _attacker = attacker;
        _opponent = opponent;
    }

    public bool IsBroken() => _attacker.CurrentSphere != _opponent.CurrentSphere;

    public string Message => "Competitiors must be within the same sphere to start a battle.";
}
