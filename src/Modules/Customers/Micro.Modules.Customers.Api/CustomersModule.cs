using Micro.Abstractions.Modules;
using Micro.Abstractions.Handlers;
using Microsoft.AspNetCore.Builder;
using Micro.Modules.Customers.Core;
using Micro.Modules.Customers.Application;
using Micro.Modules.Customers.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Micro.Modules.Customers.Application.Customers.Queries;
using Micro.Modules.Customers.Application.Customers.DTO;
using Micro.Modules.Customers.Application.Customers;
using Micro.Abstractions.Pagination;

namespace Micro.Modules.Customers.Api;

internal class CustomersModule : IModule
{
    public string Name { get; } = "Customers";

    public IEnumerable<string> Policies { get; } = new[]
    {
        "transfers", "customers"
    };

    public void Register(IServiceCollection services)
    {
        services
        .AddDomain()
            .AddApplication()
            .AddInfrastructure();
    }

    public void Use(IApplicationBuilder app)
    {

        app.UseModuleRequests()
             .Subscribe<GetCustomer, CustomerDetailsDto>("customers/get",
                 (query, serviceProvider, cancellationToken)
                     => serviceProvider.GetRequiredService<IQueryDispatcher>().QueryAsync(query, cancellationToken))
             .Subscribe<BrowseCustomers, Paged<CustomerDto>>("customers/customers",
             (query, serviceprovider, cancellationToken)
             => serviceprovider.GetRequiredService<IQueryDispatcher>().QueryAsync(query, cancellationToken));
    }

}