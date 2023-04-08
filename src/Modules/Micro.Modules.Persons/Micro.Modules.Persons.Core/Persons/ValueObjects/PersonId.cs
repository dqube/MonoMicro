namespace Micro.Modules.Persons.Core.Persons.ValueObjects
{
    internal record struct PersonId
    {
        public int Value { get; }

        public PersonId(int value)
        {
            Value = value;
        }

        public static implicit operator int(PersonId personId)
            => personId.Value;

        public static implicit operator PersonId(int value)
            => new(value);
    }
}