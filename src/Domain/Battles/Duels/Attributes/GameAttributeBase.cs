using Domain.Shared;

namespace Domain.Battles.Duels.Attributes;

public abstract record GameAttributeBase : ValueObjectBase
{
    public string Name { get; set; }
    protected GameAttributeBase(string name)
    {
        Name = name;
    }
}
