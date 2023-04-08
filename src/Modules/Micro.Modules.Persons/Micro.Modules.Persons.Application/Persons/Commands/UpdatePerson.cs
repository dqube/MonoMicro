using Micro.Abstractions.Abstractions;
using Micro.Modules.Persons.Core.Persons.ValueObjects;

namespace Micro.Modules.Persons.Application.Persons
{
    internal record UpdatePerson(int personId, string Name) : ICommand
    {
    }
}