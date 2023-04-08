using Micro.Abstractions.Handlers;
using Micro.Abstractions.Pagination;
using Micro.DAL.SqlServer;
using Micro.Modules.Persons.Application.Persons.DTO;
using Micro.Modules.Persons.Application.Persons.Queries;
using Micro.Modules.Persons.Infrastructure.DAL;
using Micro.Modules.Persons.Infrastructure.DAL.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Micro.Modules.Persons.Core.Queries.Handlers
{
    internal sealed class BrowsePersonsHandler : IQueryHandler<BrowsePersons, Paged<PersonDto>>
    {
        private readonly PersonsDbContext _dbContext;

        public BrowsePersonsHandler(PersonsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Paged<PersonDto>> HandleAsync(BrowsePersons query, CancellationToken cancellationToken = default)
        {
            var persons = _dbContext.Persons.AsQueryable();

            return persons.AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => x.AsDto())
                .PaginateAsync(query, cancellationToken);
        }
    }
}