namespace WebApp.Contract.V1.Battles.ListBattles;

public sealed record ListBattlesResponse
{
    public BattleResultDto[] Battles { get; set; } = [];
}
