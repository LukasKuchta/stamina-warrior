namespace WebApp.Contract.V1.Battles.ListBattles;

public sealed record ListBattlesResponse
{
    public BattleSummaryDto[] Battles { get; set; } = [];
}
