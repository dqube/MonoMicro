using Micro.Abstractions.Modules;
using Micro.Modules.Users.Application.Clients.Customers;
using Micro.Modules.Users.Application.Clients.Customers.DTO;
using Micro.Modules.Users.Infrastructure.Clients.Requests;

namespace Micro.Modules.Users.Infrastructure.Clients
{
    internal sealed class CustomerApiClient : ICustomerApiClient
    {
        private readonly IModuleClient _client;

        public CustomerApiClient(IModuleClient client)
        {
            _client = client;
        }
        public Task<IEnumerable<CustomerDto>> GetCustomersAsync(int userId)
         => _client.SendAsync<IEnumerable<CustomerDto>>("users/users/get", new GetCustomer
         {
             CustomerId = userId
         });

        public Task<CustomerDto> GetCustomerAsync(int id)
        => _client.SendAsync<CustomerDto>("users/get",
                    new CustomerDto
                    {
                        Id = id
                    });

        public Task<CustomerDto> GetCustomerByMail(string email)
       => _client.SendAsync<CustomerDto>("users/getbymail", new { email });
    }
}