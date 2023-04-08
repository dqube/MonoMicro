using Micro.Modules.Customers.Core.Customers.Entities;
using Micro.Modules.Customers.Core.Customers.ValueObjects;

namespace Micro.Modules.Customers.Core.Customers.Repositories;

internal interface ICustomerRepository
{
    Task<Customer> GetAsync(CustomerId id);
    Task AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
}
