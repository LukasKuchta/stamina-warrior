namespace WebApp.Contract.V1.Warriors.ListWarriors;

public sealed record ListWarriorsResponse
{
    public WarriorSummaryDto[] Warriors { get; set; } = [];
}
