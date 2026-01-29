
using Domain.MagicCards.Cards;
using Domain.Shared;

namespace Domain.MagicCards;

public abstract record MagicCardBase : ValueObjectBase
{
    public string Name { get; init; }
    public Chance ActivationChance { get; init; }    

    protected MagicCardBase(string name, Chance activationChance)
    {
        Name = name;
        ActivationChance = activationChance;
    }
}
