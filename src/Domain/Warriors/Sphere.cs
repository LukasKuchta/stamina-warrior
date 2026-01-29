using Domain.Shared;

namespace Domain.Warriors;

public sealed record Sphere : ValueObjectBase
{
    public static readonly Sphere BlueSky = new(0, "BlueSky", 0.25f);
    public static readonly Sphere Betweenworld = new(1, "Betweenworld", 0.2f);
    public static readonly Sphere Deepvault = new(2, "Deepvault", 0.1f);

    private Sphere(int difficulty, string name, float hitRatio)
    {
        Difficulty = difficulty;
        Name = name;
        HitRatio = hitRatio;
    }

    public string Name { get; init; }
    public int Difficulty { get; init; }
    public float HitRatio { get; init; }

    public static Sphere FromString(string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        var sphere = All.FirstOrDefault(s => s.Name.Equals(value.Trim(), StringComparison.OrdinalIgnoreCase));
        return sphere ?? throw new ArgumentException($"Invalid sphere value: {value}");
    }

    public static IReadOnlyCollection<Sphere> All { get; } = [BlueSky, Betweenworld, Deepvault];
}
