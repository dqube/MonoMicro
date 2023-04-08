using Micro.Modules.Persons.Application.Clients.Users.DTO;

namespace Micro.Modules.Persons.Application.Clients.Users
{
    internal interface IUserApiClient
    {
        Task<UserDto> GetUserByMail(string email);
        Task<UserDto> GetUserAsync(int id);
        Task<IEnumerable<UserDto>> GetUsersAsync(int conferenceId);
    }
}