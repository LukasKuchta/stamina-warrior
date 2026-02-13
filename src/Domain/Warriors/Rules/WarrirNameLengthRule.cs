using Domain.Shared;

namespace Domain.Warriors.Rules;

public sealed class WarrirNameLengthRule(string name) : IBusinessRule
{
    public string Message => $"Invalid warrior's name! [{name}]";

    public bool IsBroken()
    {
        return name.Length < 2 || name.Length > 50;
    }
}
