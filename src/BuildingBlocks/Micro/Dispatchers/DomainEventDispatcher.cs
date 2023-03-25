using Micro.Kernel;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.Dispatchers;

public sealed class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventDispatcher(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public Task DispatchAsync(IDomainEvent @event, CancellationToken cancellationToken = default)
        => DispatchAsync(cancellationToken, @event);

    public Task DispatchAsync(IDomainEvent[] events, CancellationToken cancellationToken = default)
        => DispatchAsync(cancellationToken, events);
        
    private async Task DispatchAsync(CancellationToken cancellationToken, params IDomainEvent[] events)
    {
        if (events is null || !events.Any())
        {
            return;
        }

        using var scope = _serviceProvider.CreateScope();
        foreach (var @event in events)
        {
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(@event.GetType());
            var handlers = scope.ServiceProvider.GetServices(handlerType);

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            var tasks = handlers.Select(x => (Task) handlerType
                .GetMethod(nameof(IDomainEventHandler<IDomainEvent>.HandleAsync))
                ?.Invoke(x, new object[] {@event, cancellationToken}));
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            await Task.WhenAll(tasks);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
}