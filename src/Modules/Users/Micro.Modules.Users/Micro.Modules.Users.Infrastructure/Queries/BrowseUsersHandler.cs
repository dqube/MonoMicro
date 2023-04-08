using Micro.Abstractions.Handlers;
using Micro.Abstractions.Pagination;
using Micro.DAL.SqlServer;
using Micro.Modules.Users.Application.Users.DTO;
using Micro.Modules.Users.Application.Users.Queries;
using Micro.Modules.Users.Infrastructure.DAL;
using Micro.Modules.Users.Infrastructure.DAL.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Micro.Modules.Users.Core.Queries.Handlers
{
    internal sealed class BrowseUsersHandler : IQueryHandler<BrowseUsers, Paged<UserDto>>
    {
        private readonly UsersDbContext _dbContext;

        public BrowseUsersHandler(UsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Paged<UserDto>> HandleAsync(BrowseUsers query, CancellationToken cancellationToken = default)
        {
            var users = _dbContext.Users.AsQueryable();

            return users.AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => x.AsDto())
                .PaginateAsync(query, cancellationToken);
        }
    }
}