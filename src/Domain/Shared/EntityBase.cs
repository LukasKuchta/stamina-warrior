
using System;

namespace Domain.Shared;

public record DomainEventBase;

public abstract class EntityBase
{
    private readonly List<DomainEventBase> _domainEvents = new List<DomainEventBase>();

    internal void AddDomainEvent(DomainEventBase @event) 
    {
        _domainEvents.Add(@event);
    }

    public void ClearDomainEvents() 
    {
        _domainEvents.Clear();
    }

    public IReadOnlyList<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

    protected static void CheckRule(IBusinessRule rule)
    {
        ArgumentNullException.ThrowIfNull(rule);

        if (rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }
}
