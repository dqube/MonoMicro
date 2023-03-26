﻿using $safeprojectname$.Kernel;

namespace $safeprojectname$.Kernel.Types;

public abstract class AggregateRoot<T>
{
    public T Id { get; protected set; }
    public int Version { get; protected set; }

    public IEnumerable<IDomainEvent> Events => _events;
    private List<IDomainEvent> _events = new();

    private bool _versionIncremented;

    protected void AddEvent(IDomainEvent @event)
    {
        if (!_events.Any() && !_versionIncremented) // Only one version up when update/create aggregate
        {
            Version++;
            _versionIncremented = true;
        }

        _events.Add(@event);
    }

    public void ClearEvents() => _events.Clear();

    protected void IncrementVersion()
    {
        if (_versionIncremented)
        {
            return;
        }

        Version++;
        _versionIncremented = true;
    }
}

public abstract class AggregateRoot : AggregateRoot<AggregateId>
{
}
