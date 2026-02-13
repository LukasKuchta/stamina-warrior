using Carter;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApp.Contract.V1.Battles.StartBattle;

namespace WebApp.Modules.Battles;

public sealed class StarteBattleModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var battles = app.MapBattlesV1();

        battles.MapPost("/", Handle)
                .Produces<StartteBattleResponse>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status409Conflict);
    }

    private static async Task<Results<Created<StartteBattleResponse>, BadRequest, Conflict>> Handle(StartBattleRequest request)
    {
        _ = request;
        // add validation here 
        // return Results.BadRequest(new { error = "...

        // check player exists
        // return Results.NotFound();

        // check if playr is already in battle
        // return Results.Conflict(new { error = "Player is already in battle." });

        var response = new StartteBattleResponse();
        var id = Guid.NewGuid();        

        return TypedResults.Created($"/api/v1/battles/{id}", response);
    }
}
