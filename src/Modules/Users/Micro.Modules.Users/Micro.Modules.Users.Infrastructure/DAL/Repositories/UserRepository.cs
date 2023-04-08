using Micro.Modules.Users.Core.Users.Entities;
using Micro.Modules.Users.Core.Users.Repositories;
using Micro.Modules.Users.Core.Users.ValueObjects;
using Micro.Modules.Users.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace Micro.Modules.Users.Infrastructure.DAL.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly UsersDbContext _context;
        private readonly DbSet<User> _users;

        public UserRepository(UsersDbContext context)
        {
            _context = context;
            _users = _context.Users;
        }

        public Task<User> GetAsync(UserId id)
            => _users
               .SingleOrDefaultAsync(x => x.Id == id);



        public async Task AddAsync(User user)
        {
            await _users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}