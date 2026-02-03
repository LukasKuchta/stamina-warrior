using Domain.Shared;
using Domain.Warriors.Rules;

namespace Domain.Warriors;

public sealed record Level : ValueObjectBase
{
    public int Value { get; private set; }

    private Level(int value)
    {
        Value = value;
    }     

    public static Level FromNumber(int value)
    {
        CheckRule(new LevelCannotBeNegativeRule(value));

        return new Level(value);
    }
}
