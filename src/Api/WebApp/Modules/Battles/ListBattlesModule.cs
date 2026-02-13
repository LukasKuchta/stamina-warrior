using Carter;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApp.Contract.V1.Battles.ListBattles;

namespace WebApp.Modules.Battles;

public sealed class ListBattlesModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var battles = app.MapBattlesV1();

        battles.MapGet("/", Handle).Produces<ListBattlesResponse>(StatusCodes.Status200OK);
    }

    private static Results<Ok<ListBattlesResponse>, BadRequest> Handle()
    {
        var response = new ListBattlesResponse();
        return TypedResults.Ok(response);
    }
}
