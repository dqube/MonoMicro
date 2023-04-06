using Micro.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Micro.Modules.Customers.Core;
using Micro.Modules.Customers.Application;
using Micro.Modules.Customers.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

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

    }

}