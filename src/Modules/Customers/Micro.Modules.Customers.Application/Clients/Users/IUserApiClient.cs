using Micro.Modules.Customers.Application.Clients.Users.DTO;

namespace Micro.Modules.Customers.Application.Clients.Users;

public interface IUserApiClient
{
    Task<UserDto> GetRegularAgendaSlotAsync(int id);
    Task<IEnumerable<UserDto>> GetAgendaAsync(int conferenceId);
}
