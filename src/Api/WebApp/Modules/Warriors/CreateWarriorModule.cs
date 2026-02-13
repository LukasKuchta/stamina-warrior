using Carter;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApp.Contract.V1.Battles.StartBattle;
using WebApp.Contract.V1.Warriors.CreateWarrior;

namespace WebApp.Modules.Warriors;

public sealed class CreateWarriorModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var battles = app.MapWarriorsV1();

        battles.MapPost("/", Handle)
                .Produces<Guid>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest);                
    }

    private static async Task<Results<Created<Guid>, BadRequest>> Handle(CreateWarriorRequest request)
    {
        _ = request;
        // add validation here 
        // return Results.BadRequest(new { error = "...
       
        var id = Guid.NewGuid();

        return TypedResults.Created($"/api/v1/warriors/{id}", id);
    }
}
