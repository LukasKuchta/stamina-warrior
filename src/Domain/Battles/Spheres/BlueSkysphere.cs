namespace Domain.Battles.Spheres;

public sealed record BlueSkysphere : SphereBase
{
    internal BlueSkysphere(int difficulty, float hitRatio) : base("BlueSky", difficulty, hitRatio)
    {

    }
}

