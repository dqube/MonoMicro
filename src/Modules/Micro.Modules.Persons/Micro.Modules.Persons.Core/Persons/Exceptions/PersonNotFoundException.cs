using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Persons.Core.Persons.Exceptions
{
    public class PersonNotFoundException : CustomException
    {
        public int PersonId { get; }

        public PersonNotFoundException(int personId) : base($"Person with ID: '{personId}' was not found.")
        {
            PersonId = personId;
        }

    }
}