using Microsoft.Extensions.DependencyInjection;

namespace Micro.Modules.Customers.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services;
           
            //.AddScoped<IWalletRepository, WalletRepository>()
            //.AddSqlServerModule<WalletsDbContext>()
            //.AddUnitOfWork<WalletsUnitOfWork>();
    }
}