namespace Micro.Modules.Users.Core.Users.ValueObjects
{
    internal record struct WalletId
    {
        public Guid Value { get; }

        public WalletId(Guid value)
        {
            Value = value;
        }

        public static implicit operator WalletId(Guid value) => new(value);
        public static implicit operator Guid(WalletId id) => id.Value;
    }
    public sealed record Capacity
    {
        public int Value { get; }

        public Capacity(int value)
        {
            if (value is < 0 or > 4)
            {
                //throw new InvalidCapacityException(value);
            }

            Value = value;
        }

        public static implicit operator int(Capacity capacity)
            => capacity.Value;

        public static implicit operator Capacity(int value)
            => new(value);
    }
}