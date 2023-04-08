using Micro.Abstractions.Handlers;
using Micro.Modules.Users.Application.Users.DTO;
using Micro.Modules.Users.Application.Users.Queries;
using Micro.Modules.Users.Infrastructure.DAL;
using Micro.Modules.Users.Infrastructure.DAL.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Micro.Modules.Users.Core.Queries.Handlers
{
    internal sealed class GetUserHandler : IQueryHandler<GetUser, UserDetailsDto>
    {
        private readonly UsersDbContext _dbContext;

        public GetUserHandler(UsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserDetailsDto> HandleAsync(GetUser query, CancellationToken cancellationToken = default)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == query.UserId, cancellationToken);

            return user.AsDetailsDto();
        }
    }
}