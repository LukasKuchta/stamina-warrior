using Carter;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApp.Contract.V1.Battles.BattleDetail;

namespace WebApp.Modules.Battles;

public sealed class GetBattleModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var battles = app.MapBattlesV1();

        battles.MapGet("/{id:guid}", Handle)
               .Produces<GetBattleResponse>(StatusCodes.Status200OK)
               .Produces(StatusCodes.Status404NotFound);
    }

    private static async Task<Results<Ok<GetBattleResponse>, NotFound>> Handle(Guid id)
    {
        _ = id;
        var response = new GetBattleResponse();
        return TypedResults.Ok(response);
    }
}
