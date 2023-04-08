using Micro.Abstractions.Abstractions;
using Micro.Modules.Persons.Core.Persons.ValueObjects;

namespace Micro.Modules.Persons.Application.Persons
{
    internal record AddPerson(PersonId personId, string Name) : ICommand
    {
    }
}