using System.ComponentModel.DataAnnotations.Schema;

namespace Micro.Abstractions.Kernel.Types;
public abstract class Aggregate<TId> : Entity<TId>
    where TId : notnull
{
   

    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    public new TId Id { get; protected set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected Aggregate()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        
    }
    protected Aggregate(TId id):base(id) 
    {
        Id = id;    
    }
   

    

   
}