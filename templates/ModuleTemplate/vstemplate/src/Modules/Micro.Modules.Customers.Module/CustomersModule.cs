using Micro.Abstractions.Modules;
using Micro.Modules.Customers.Application;
using Micro.Modules.Customers.Domain;
using Micro.Modules.Customers.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace $safeprojectname$;

internal class CustomersModule : IModule
{
    public string Name { get; } = "Customers";

    public IEnumerable<string> Policies { get; } = new[]
    {
        "deposits", "withdrawals"
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
