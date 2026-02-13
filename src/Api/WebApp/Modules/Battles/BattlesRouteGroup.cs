namespace WebApp.Modules.Battles;

public static class BattlesRouteGroup
{
    public static RouteGroupBuilder MapBattlesV1(this IEndpointRouteBuilder app)
        => app.MapGroup("/api/v1/battles")
              .WithTags("Battles");
}
