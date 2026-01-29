
using Domain.Shared;

namespace Domain.Warriors.Rules;
internal sealed class LevelCannotBeNegativeRule(int level) : IBusinessRule
{
    public string Message => "Level cannot be negative!";

    public bool IsBroken()
    {
        return level < 0;
    }
}
