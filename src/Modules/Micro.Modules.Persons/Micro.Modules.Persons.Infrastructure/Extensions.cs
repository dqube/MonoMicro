using Micro.DAL.SqlServer;
using Micro.Modules.Persons.Application.Clients.Users;
using Micro.Modules.Persons.Core.Persons.Repositories;
using Micro.Modules.Persons.Infrastructure.Clients;
using Micro.Modules.Persons.Infrastructure.DAL;
using Micro.Modules.Persons.Infrastructure.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.Modules.Persons.Infrastructure
{
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services
                .AddSingleton<IUserApiClient, UserApiClient>()
                .AddScoped<IPersonRepository, PersonRepository>()
                .AddSqlServerModule<PersonsDbContext>()
                .AddUnitOfWork<PersonsUnitOfWork>();
        }
    }
}