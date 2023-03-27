using Micro.Abstractions;
using Micro.Abstractions.Kernel;

namespace Micro.Abstractions.Kernel.Types;

public interface IAggregate : IEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
    long Version { get; set; }
}

public interface IAggregate<out TId> : IAggregate
{
    TId Id { get; }
}
