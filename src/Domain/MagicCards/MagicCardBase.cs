using Domain.ActivationRules;
using Domain.Shared;

namespace Domain.MagicCards;

public abstract record MagicCardBase : ValueObjectBase
{
    public string Name { get; }

    protected MagicCardBase(string name) => Name = name;    
}


