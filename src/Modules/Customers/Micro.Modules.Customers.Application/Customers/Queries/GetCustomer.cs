using Micro.Abstractions.Abstractions;
using Micro.Modules.Customers.Application.Customers.DTO;

namespace Micro.Modules.Customers.Application.Customers.Queries;

internal class GetCustomer : IQuery<CustomerDetailsDto>
{
    public int CustomerId { get; set; }
}