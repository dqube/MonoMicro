using Micro.Modules.Users.Core.Users.Entities;
using Micro.Modules.Users.Core.Users.ValueObjects;

namespace Micro.Modules.Users.Core.Users.Repositories
{
    internal interface IUserRepository
    {
        Task<User> GetAsync(UserId id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}