namespace Domain.Battles.Spheres;

public sealed record BetweenworldSphere : SphereBase
{
    internal BetweenworldSphere(int difficulty, float hitRatio) : base("Betweenworld", difficulty, hitRatio)
    {

    }
}

