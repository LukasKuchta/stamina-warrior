namespace WebApp.Contract.V1.Battles.StartBattle;

public sealed record StartBattleRequest
{
    public WarriorDto Attacker { get; set; }
    public WarriorDto Oponent { get; set; }
}
