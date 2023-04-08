using Micro.Abstractions.Pagination;
using Micro.Modules.Persons.Application.Persons.DTO;

namespace Micro.Modules.Persons.Application.Persons.Queries
{
    internal class BrowsePersons : PagedQuery<PersonDto>
    {
        public int PersonId { get; set; }
        public string? Name { get; set; }
    }
}