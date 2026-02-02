
using System;
using System.Collections.Immutable;

namespace Domain.Shared;

public record DomainEventBase;

public abstract class EntityBase
{
    private readonly List<DomainEventBase> _domainEvents = new List<DomainEventBase>();
    private readonly IReadOnlyList<DomainEventBase> _domainEventsView;

    protected EntityBase()
    {
        _domainEventsView = _domainEvents.AsReadOnly();
    }

    protected void AddDomainEvent(DomainEventBase @event)
    {
        _domainEvents.Add(@event);
    }

    public ImmutableArray<DomainEventBase> DequeueDomainEvents()
    {        
        if (_domainEvents.Count == 0)
        {
            return ImmutableArray<DomainEventBase>.Empty;
        }

        var snapshot = _domainEvents.ToImmutableArray();
        _domainEvents.Clear();
        return snapshot;
    }

    internal IReadOnlyList<DomainEventBase> DomainEvents => _domainEventsView;

    protected static void CheckRule(IBusinessRule rule)
    {
        ArgumentNullException.ThrowIfNull(rule);

        if (rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }
}
