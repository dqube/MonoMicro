using Micro.Abstractions.Kernel.Types;
using Micro.Modules.Persons.Core.Persons.ValueObjects;

namespace Micro.Modules.Persons.Core.Persons.Entities
{
    internal class Person : Aggregate<PersonId>
    {
        private Person(PersonId id, string name) : base(id)
        {
            Name = name;
            Id = id;
        }


        public string Name { get; private set; }
        public PersonId Id { get; private set; }

        public static Person Create(PersonId personId, string name)
        {
            var person = new Person(personId, name);
            return person;
        }
    }
}