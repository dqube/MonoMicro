using Micro.Abstractions.Handlers;
using Micro.Modules.Persons.Application.Persons.DTO;
using Micro.Modules.Persons.Application.Persons.Queries;
using Micro.Modules.Persons.Infrastructure.DAL;
using Micro.Modules.Persons.Infrastructure.DAL.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Micro.Modules.Persons.Core.Queries.Handlers
{
    internal sealed class GetPersonHandler : IQueryHandler<GetPerson, PersonDetailsDto>
    {
        private readonly PersonsDbContext _dbContext;

        public GetPersonHandler(PersonsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PersonDetailsDto?> HandleAsync(GetPerson query, CancellationToken cancellationToken = default)
        {
            var person = await _dbContext.Persons
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == query.PersonId, cancellationToken);

            return person?.AsDetailsDto();
        }
    }
}