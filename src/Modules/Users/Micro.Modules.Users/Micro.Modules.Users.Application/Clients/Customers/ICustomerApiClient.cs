using Micro.Modules.Users.Application.Clients.Customers.DTO;

namespace Micro.Modules.Users.Application.Clients.Customers
{
    internal interface ICustomerApiClient
    {
        Task<CustomerDto> GetCustomerByMail(string email);
        Task<CustomerDto> GetCustomerAsync(int id);
        Task<IEnumerable<CustomerDto>> GetCustomersAsync(int conferenceId);
    }
}