using Micro.Abstractions.Abstractions;

namespace Micro.Modules.Customers.Application.Customers.Events;

internal record CustomerAdded(int CustomerId, string Name) : IEvent;