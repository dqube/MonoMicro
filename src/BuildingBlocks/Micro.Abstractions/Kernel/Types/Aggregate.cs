using Micro.Abstractions;
using Micro.Abstractions.Kernel;

namespace Micro.Abstractions.Kernel.Types;

public abstract class Aggregate : Aggregate<long>
{
}

public abstract class Aggregate<TId> : Entity, IAggregate<TId>
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    //public IEvent[] ClearDomainEvents()
    //{
    //    IEvent[] dequeuedEvents = _domainEvents.ToArray();

    //    _domainEvents.Clear();

    //    return dequeuedEvents;
    //}
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    public long Version { get; set; } = -1;

    public TId Id { get; protected set; }
}
