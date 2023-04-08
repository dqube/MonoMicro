using Micro.Modules.Persons.Core.Persons.Entities;
using Micro.Modules.Persons.Core.Persons.ValueObjects;

namespace Micro.Modules.Persons.Core.Persons.Repositories
{
    internal interface IPersonRepository
    {
        Task<Person> GetAsync(PersonId id);
        Task AddAsync(Person person);
        Task UpdateAsync(Person person);
    }
}