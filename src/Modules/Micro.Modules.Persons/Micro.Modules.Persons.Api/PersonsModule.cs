using Micro.Abstractions.Handlers;
using Micro.Abstractions.Modules;
using Micro.Abstractions.Pagination;
using Micro.Modules.Persons.Application;
using Micro.Modules.Persons.Application.Persons;
using Micro.Modules.Persons.Application.Persons.DTO;
using Micro.Modules.Persons.Application.Persons.Queries;
using Micro.Modules.Persons.Core;
using Micro.Modules.Persons.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.Modules.Persons.Api
{
    internal class PersonsModule : IModule
    {
        public string Name { get; } = "Persons";

        public IEnumerable<string> Policies { get; } = new[]
        {
        "transfers", "persons"
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
                 .Subscribe<GetPerson, PersonDetailsDto>("persons/get",
                     (query, serviceProvider, cancellationToken)
                         => serviceProvider.GetRequiredService<IQueryDispatcher>().QueryAsync(query, cancellationToken))
                 .Subscribe<BrowsePersons, Paged<PersonDto>>("persons/persons",
                 (query, serviceprovider, cancellationToken)
                 => serviceprovider.GetRequiredService<IQueryDispatcher>().QueryAsync(query, cancellationToken));
        }

    }
}