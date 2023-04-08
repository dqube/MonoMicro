using Micro.Abstractions.Abstractions;

namespace Micro.Modules.Persons.Application.Persons.Events
{
    internal record PersonAdded(int PersonId, string Name) : IEvent;
}