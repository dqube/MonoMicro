using $safeprojectname$;
using $safeprojectname$.Kernel;

namespace $safeprojectname$.Kernel.Types;

public interface IAggregate : IEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    //IEvent[] ClearDomainEvents();
    void ClearDomainEvents();
    long Version { get; set; }
}

public interface IAggregate<out T> : IAggregate
{
    T Id { get; }
}
