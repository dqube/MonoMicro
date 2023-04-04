namespace $safeprojectname$.Kernel.Types;
public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
    where TId : notnull
{

    public int DomainEventVersion { get; protected set; }
    public long Version { get; set; } = -1;

    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    private bool _versionIncremented;
    protected Aggregate(TId id) : base(id)
    {
    }
    protected void AddDomainEvent(IDomainEvent @event)
    {
        if (!_domainEvents.Any() && !_versionIncremented) // Only one version up when update/create aggregate
        {
            DomainEventVersion++;
            _versionIncremented = true;
        }

        _domainEvents.Add(@event);
    }

    public void ClearDomainEvents() => _domainEvents.Clear();

    protected void IncrementVersion()
    {
        if (_versionIncremented)
        {
            return;
        }

        DomainEventVersion++;
        _versionIncremented = true;
    }
}