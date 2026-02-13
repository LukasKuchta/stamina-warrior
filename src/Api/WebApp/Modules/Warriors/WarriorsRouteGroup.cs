namespace WebApp.Modules.Warriors;

public static class WarriorsRouteGroup
{
    public static RouteGroupBuilder MapWarriorsV1(this IEndpointRouteBuilder app)
    => app.MapGroup("/api/v1/warriors")
          .WithTags("Warriors");
}
