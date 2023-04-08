using BASEREF-NAME.Abstractions.Handlers;
using BASEREF-NAME.Abstractions.Pagination;
using BASEREF-NAME.DAL.SqlServer;
using Micro.Modules.Customers.Application.Customers.DTO;
using Micro.Modules.Customers.Application.Customers.Queries;
using Micro.Modules.Customers.Infrastructure.DAL;
using Micro.Modules.Customers.Infrastructure.DAL.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Micro.Modules.Customers.Core.Queries.Handlers;

internal sealed class BrowseCustomersHandler : IQueryHandler<BrowseCustomers, Paged<CustomerDto>>
{
    private readonly CustomersDbContext _dbContext;

    public BrowseCustomersHandler(CustomersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Paged<CustomerDto>> HandleAsync(BrowseCustomers query, CancellationToken cancellationToken = default)
    {
        var customers = _dbContext.Customers.AsQueryable();
       
        return customers.AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => x.AsDto())
            .PaginateAsync(query,cancellationToken);
    }
}