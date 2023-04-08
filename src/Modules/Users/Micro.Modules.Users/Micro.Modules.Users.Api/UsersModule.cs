using Micro.Abstractions.Handlers;
using Micro.Abstractions.Modules;
using Micro.Abstractions.Pagination;
using Micro.Modules.Users.Application;
using Micro.Modules.Users.Application.Users;
using Micro.Modules.Users.Application.Users.DTO;
using Micro.Modules.Users.Application.Users.Queries;
using Micro.Modules.Users.Core;
using Micro.Modules.Users.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.Modules.Users.Api
{
    internal class UsersModule : IModule
    {
        public string Name { get; } = "Users";

        public IEnumerable<string> Policies { get; } = new[]
        {
        "transfers", "users"
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
                 .Subscribe<GetUser, UserDetailsDto>("users/get",
                     (query, serviceProvider, cancellationToken)
                         => serviceProvider.GetRequiredService<IQueryDispatcher>().QueryAsync(query, cancellationToken))
                 .Subscribe<BrowseUsers, Paged<UserDto>>("users/users",
                 (query, serviceprovider, cancellationToken)
                 => serviceprovider.GetRequiredService<IQueryDispatcher>().QueryAsync(query, cancellationToken));
        }

    }
}