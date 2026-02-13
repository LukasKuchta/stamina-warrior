using Carter;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApp.Contract.V1.Battles.GetBattle;

namespace WebApp.Modules.Battles;

public sealed class BattleDetailsModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var battles = app.MapBattlesV1();

        battles.MapGet("/{id:guid}", Handle)
               .Produces<BattleDetailDto>(StatusCodes.Status200OK)
               .Produces(StatusCodes.Status404NotFound);
    }

    private static async Task<Results<Ok<BattleDetailDto>, NotFound>> Handle(Guid id)
    {
        _ = id;
        var response = new BattleDetailDto();
        return TypedResults.Ok(response);
    }
}
