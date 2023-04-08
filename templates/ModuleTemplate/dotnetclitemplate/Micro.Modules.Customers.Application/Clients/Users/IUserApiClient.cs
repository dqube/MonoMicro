using Micro.Modules.Customers.Application.Clients.Users.DTO;

namespace Micro.Modules.Customers.Application.Clients.Users;

internal interface IUserApiClient
{
    Task<UserDto> GetUserByMail(string email);
    Task<UserDto> GetUserAsync(int id);
    Task<IEnumerable<UserDto>> GetUsersAsync(int conferenceId);
}
