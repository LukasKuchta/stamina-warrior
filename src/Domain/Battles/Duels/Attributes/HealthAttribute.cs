namespace Domain.Battles.Duels.Attributes;

public sealed record HealthAttribute : GameAttributeBase
{
    internal HealthAttribute(GameAttributeBase original) : base(original)
    {
    }
}
