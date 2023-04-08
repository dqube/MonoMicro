namespace Micro.Modules.Users.Core.Users.ValueObjects
{
    internal record struct UserId
    {
        public int Value { get; }

        public UserId(int value)
        {
            Value = value;
        }

        public static implicit operator int(UserId userId)
            => userId.Value;

        public static implicit operator UserId(int value)
            => new(value);
    }
}