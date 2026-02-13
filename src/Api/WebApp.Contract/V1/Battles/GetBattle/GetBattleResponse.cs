using WebApp.Contract.V1.Battles.GetBattle;

namespace WebApp.Contract.V1.Battles.BattleDetail;

public sealed record GetBattleResponse
{
    public BattleDetailDto Detail { get; set; }
}
