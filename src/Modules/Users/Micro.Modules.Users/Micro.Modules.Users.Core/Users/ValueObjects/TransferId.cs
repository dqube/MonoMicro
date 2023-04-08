namespace Micro.Modules.Users.Core.Users.ValueObjects
{

    internal record struct TransferId
    {
        public Guid Value { get; }

        public TransferId(Guid value)
        {
            Value = value;
        }

        public static implicit operator TransferId(Guid value) => new(value);
        public static implicit operator Guid(TransferId id) => id.Value;
    }
}