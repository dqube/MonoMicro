namespace Micro.Abstractions.Kernel.Types;

public interface IEntity
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    void AddDomainEvent(IDomainEvent domainEvent);

    void RemoveDomainEvent(IDomainEvent domainEvent);

    long Version { get; set; }

    void ClearDomainEvents();
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public long? LastModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
}
