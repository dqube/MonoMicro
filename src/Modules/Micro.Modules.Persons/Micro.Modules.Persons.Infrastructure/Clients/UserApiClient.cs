using Micro.Abstractions.Modules;
using Micro.Modules.Persons.Application.Clients.Users;
using Micro.Modules.Persons.Application.Clients.Users.DTO;
using Micro.Modules.Persons.Infrastructure.Clients.Requests;

namespace Micro.Modules.Persons.Infrastructure.Clients
{
    internal sealed class UserApiClient : IUserApiClient
    {
        private readonly IModuleClient _client;

        public UserApiClient(IModuleClient client)
        {
            _client = client;
        }
        public Task<IEnumerable<UserDto>> GetUsersAsync(int userId)
         => _client.SendAsync<IEnumerable<UserDto>>("users/users/get", new GetUser
         {
             UserId = userId
         });

        public Task<UserDto> GetUserAsync(int id)
        => _client.SendAsync<UserDto>("users/get",
                    new UserDto
                    {
                        Id = id
                    });

        public Task<UserDto> GetUserByMail(string email)
       => _client.SendAsync<UserDto>("users/getbymail", new { email });
    }
}