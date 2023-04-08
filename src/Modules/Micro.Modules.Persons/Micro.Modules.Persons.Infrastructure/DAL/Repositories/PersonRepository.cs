using Micro.Modules.Persons.Core.Persons.Entities;
using Micro.Modules.Persons.Core.Persons.Repositories;
using Micro.Modules.Persons.Core.Persons.ValueObjects;
using Micro.Modules.Persons.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace Micro.Modules.Persons.Infrastructure.DAL.Repositories
{
    internal class PersonRepository : IPersonRepository
    {
        private readonly PersonsDbContext _context;
        private readonly DbSet<Person> _persons;

        public PersonRepository(PersonsDbContext context)
        {
            _context = context;
            _persons = _context.Persons;
        }

        public Task<Person> GetAsync(PersonId id)
            => _persons
               .SingleOrDefaultAsync(x => x.Id == id);



        public async Task AddAsync(Person person)
        {
            await _persons.AddAsync(person);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Person person)
        {
            _persons.Update(person);
            await _context.SaveChangesAsync();
        }
    }
}