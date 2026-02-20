using Domain.Shared;

namespace Domain.Battles.Duels;

public sealed class CannotBeNegativeRule(int value, string propertyName) : IBusinessRule
{
    public string Message => $"{propertyName} cannot be negative.";

    public bool IsBroken()
    {
        return value < 0;
    }
}

public sealed class CheckPercentageRangeRule(int value) : IBusinessRule
{
    public string Message => "Percentage must be 0..100.";

    public bool IsBroken()
    {
        return value is < 0 or > 100;
    }
}

public sealed class CheckAddPercentageRangeRule(int value) : IBusinessRule
{
    public string Message => "Percentage must be -100..100.";

    public bool IsBroken()
    {
        return value is < -100 or > 100;
    }
}


public sealed class MustBePositive(int value, string propertyName) : IBusinessRule
{
    public string Message => $"Value {propertyName} must be positive.";

    public bool IsBroken()
    {
        return value <= 0;
    }
}
