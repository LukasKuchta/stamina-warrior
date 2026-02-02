using System.Collections.ObjectModel;
using Domain.Shared;
using Domain.Warriors;

namespace Domain.Battles.Spheres;

public abstract record SphereBase : ValueObjectBase
{
    public static readonly SphereBase BlueSky = new BlueSkysphere(0, 0.25f);
    public static readonly SphereBase Betweenworld = new BetweenworldSphere(1, 0.2f);
    public static readonly SphereBase Deepvault = new DeepvaultSphere(2, 0.1f);

    protected SphereBase(string name, int difficulty, float hitRatio)
    {
        Name = name;
        Difficulty = difficulty;
        HitRatio = hitRatio;
    }

    public string Name { get; }
    public int Difficulty { get; }
    public float HitRatio { get; }

    private static readonly IReadOnlyCollection<SphereBase> _all = Array.AsReadOnly([BlueSky, Betweenworld, Deepvault]);

    public static IReadOnlyCollection<SphereBase> All => _all;
}

