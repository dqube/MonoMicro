﻿using BASEREF-NAME.Abstractions.Handlers;
using Micro.Modules.Customers.Application.Customers.DTO;
using Micro.Modules.Customers.Infrastructure.DAL;
using Micro.Modules.Customers.Application.Customers.Queries;
using Microsoft.EntityFrameworkCore;
using Micro.Modules.Customers.Infrastructure.DAL.Mappings;

namespace Micro.Modules.Customers.Core.Queries.Handlers;

internal sealed class GetCustomerHandler : IQueryHandler<GetCustomer, CustomerDetailsDto>
{
    private readonly CustomersDbContext _dbContext;

    public GetCustomerHandler(CustomersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CustomerDetailsDto?> HandleAsync(GetCustomer query, CancellationToken cancellationToken = default)
    {
        var customer = await _dbContext.Customers
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == query.CustomerId, cancellationToken);

        return customer?.AsDetailsDto();
    }
}