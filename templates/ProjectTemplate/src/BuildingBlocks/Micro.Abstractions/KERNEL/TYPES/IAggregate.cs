using $safeprojectname$;
using $safeprojectname$.Kernel;

namespace $safeprojectname$.Kernel.Types;

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
