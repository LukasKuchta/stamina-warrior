using Carter;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApp.Contract.V1.Battles.ListBattles;
using WebApp.Contract.V1.Warriors.ListWarriors;
using WebApp.Modules.Battles;

namespace WebApp.Modules.Warriors;

public sealed class ListWarriorsModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var battles = app.MapWarriorsV1();

        battles.MapGet("/", Handle).Produces<ListWarriorsResponse>(StatusCodes.Status200OK);
    }

    private static Results<Ok<ListWarriorsResponse>, BadRequest> Handle()
    {
        var response = new ListWarriorsResponse();
        return TypedResults.Ok(response);
    }
}
