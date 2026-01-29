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

    internal Level Up(Level current)
    {
        // add invariants check
        return FromNumber(current.Value + 1);
    }

    internal Level Down(Level current)
    {
        // add invariants check
        return FromNumber(current.Value - 1);
    }

    public static Level FromNumber(int value)
    {
        CheckRule(new LevelCannotBeNegativeRule(value));

        return new Level(value);
    }
}
