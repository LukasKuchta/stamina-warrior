namespace WebApp.Contract.V1.Battles.StartBattle;

public sealed record StartBattleRequest
{
    public StartBattleWarriorDto Attacker { get; set; }
    public StartBattleWarriorDto Oponent { get; set; }
}
