using Micro.Modules.Customers.Core.Customers.Entities;
using Micro.Modules.Customers.Core.Customers.ValueObjects;

namespace Micro.Modules.Customers.Core.Customers.Repositories;

internal interface ICustomerRepository
{
    Task<Customer> GetAsync(CustomerId id, CancellationToken cancellationToken = default);
    Task AddAsync(Customer customer, CancellationToken cancellationToken = default);
    Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default);
}
