using Domain.Shared;

namespace Domain.MagicCards;

public abstract record MagicCardBase : ValueObjectBase
{
    public string Name { get; }
    public Chance ActivationChance { get; }

    protected MagicCardBase(string name, Chance activationChance)
    {
        Name = name;
        ActivationChance = activationChance;
    }
}
