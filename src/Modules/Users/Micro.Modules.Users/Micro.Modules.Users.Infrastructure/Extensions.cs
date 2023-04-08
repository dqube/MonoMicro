using Micro.DAL.SqlServer;
using Micro.Modules.Users.Application.Clients.Customers;
using Micro.Modules.Users.Core.Users.Repositories;
using Micro.Modules.Users.Infrastructure.Clients;
using Micro.Modules.Users.Infrastructure.DAL;
using Micro.Modules.Users.Infrastructure.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.Modules.Users.Infrastructure
{
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services
                .AddSingleton<ICustomerApiClient, CustomerApiClient>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddSqlServerModule<UsersDbContext>()
                .AddUnitOfWork<UsersUnitOfWork>();
        }
    }
}