using Micro.Abstractions.Abstractions;
using Micro.Modules.Persons.Application.Persons.DTO;

namespace Micro.Modules.Persons.Application.Persons.Queries
{
    internal class GetPerson : IQuery<PersonDetailsDto>
    {
        public int PersonId { get; set; }
    }
}