using Micro.Abstractions.Handlers;
using Micro.Modules.Persons.Core.Persons.Entities;
using Micro.Modules.Persons.Core.Persons.Repositories;
using Microsoft.Extensions.Logging;

namespace Micro.Modules.Persons.Application.Persons.Handlers
{
    internal sealed class AddPersonHandler : ICommandHandler<AddPerson>
    {
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<AddPersonHandler> _logger;
        public AddPersonHandler(IPersonRepository personRepository, ILogger<AddPersonHandler> logger)
        {
            _personRepository = personRepository;
            _logger = logger;
        }



        public async Task HandleAsync(AddPerson command, CancellationToken cancellationToken = default)
        {
            var person = Person.Create(
               command.personId,
               command.Name
               );
            await _personRepository.AddAsync(person);
            _logger.LogInformation($"Person {command.personId} added sucessfully'.");
            //throw new NotImplementedException();
        }
    }
}