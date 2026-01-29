
using Domain.Shared;

namespace Domain.Battles.Rules;
internal sealed record CheckIfCompetitiorsAreWithinTheSameSphereRule : IBusinessRule
{
    private readonly BattleContext _battleContext;
    public CheckIfCompetitiorsAreWithinTheSameSphereRule(BattleContext battleContext)
    {
        _battleContext = battleContext;
    }

    public bool IsBroken() => _battleContext.Attacker.CurrentSphere != _battleContext.Opponent.CurrentSphere;

    public string Message => "Competitiors must be within the same sphere to start a battle.";
}
