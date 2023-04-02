using Micro.DAL.SqlServer;
using Micro.Modules.Wallets.Domain.Wallets.Repositories;
using Micro.Modules.Wallets.Infrastructure.EF;
using Micro.Modules.Wallets.Infrastructure.EF.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.Modules.Wallets.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
           
            .AddScoped<IWalletRepository, WalletRepository>()
            .AddSqlServerModule<WalletsDbContext>()
            .AddUnitOfWork<WalletsUnitOfWork>();
    }
}