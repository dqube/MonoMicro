using Micro.Modules.Persons.Application.Persons.DTO;
using Micro.Modules.Persons.Core.Persons.Entities;

namespace Micro.Modules.Persons.Infrastructure.DAL.Mappings
{
    internal static class PersonsExtensions
    {
        public static PersonDto AsDto(this Person person)
            => person.Map<PersonDto>();

        public static PersonDetailsDto AsDetailsDto(this Person person)
        {
            var dto = person.Map<PersonDetailsDto>();

            return dto;
        }

        private static T Map<T>(this Person person) where T : PersonDto, new()
            => new()
            {
                PersonId = person.Id,
                Name = person.Name,
                // CreatedAt = person.CreatedAt
            };

    }
}