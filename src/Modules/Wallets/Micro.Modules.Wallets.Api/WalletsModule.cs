using Micro.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Micro.Modules.Wallets.Domain;
using Micro.Modules.Wallets.Application;
using Micro.Modules.Wallets.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.Modules.Wallets.Api;

internal class WalletsModule : IModule
{
    public string Name { get; } = "Wallets";

    public IEnumerable<string> Policies { get; } = new[]
    {
        "transfers", "wallets"
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