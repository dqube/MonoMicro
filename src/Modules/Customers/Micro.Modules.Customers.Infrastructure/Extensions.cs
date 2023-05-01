using Micro.DAL.SqlServer;
using Micro.Modules.Customers.Application.Clients.Users;
using Micro.Modules.Customers.Core.Customers.Repositories;
using Micro.Modules.Customers.Infrastructure.Clients;
using Micro.Modules.Customers.Infrastructure.DAL;
using Micro.Modules.Customers.Infrastructure.DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.Modules.Customers.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddSingleton<IUserApiClient, UserApiClient>()
            .AddScoped<ICustomerRepository, CustomerRepository>()
            //.AddMSSqlServer<CustomersDbContext>
            .AddSqlServerModule<CustomersDbContext>()
            .AddUnitOfWork<CustomersUnitOfWork>();
    }
}