namespace Domain.Battles.Spheres;

public sealed record DeepvaultSphere : SphereBase
{
    internal DeepvaultSphere(int difficulty, float hitRatio) : base("Deepvault", difficulty, hitRatio)
    {

    }
}

