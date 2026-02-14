using Carter;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApp.Contract.V1.Warriors.WarriorDetails;

namespace WebApp.Modules.Warriors;

public sealed class WarriorDetailsModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var battles = app.MapWarriorsV1();

        battles.MapGet("/{id:guid}", Handle)
               .Produces<WarriorDetailsDto>(StatusCodes.Status200OK)
               .Produces(StatusCodes.Status404NotFound);
    }

    private static async Task<Results<Ok<WarriorDetailsDto>, NotFound>> Handle(Guid id)
    {
        _ = id;
        var response = new WarriorDetailsDto();
        return TypedResults.Ok(response);
    }
}
