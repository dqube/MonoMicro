using Micro.Abstractions.Pagination;
using Micro.Modules.Customers.Application.Customers.DTO;

namespace Micro.Modules.Customers.Application.Customers.Queries;

internal class BrowseCustomers : PagedQuery<CustomerDto>
{
    public int CustomerId { get; set; }
    public string? Name { get; set; }
}