using Micro.Modules.Orders.Core.Orders.Exceptions;

namespace Micro.Modules.Orders.Core.Orders.ValueObjects
{
    internal record Amount
    {
        public decimal Value { get; private set; }

        public Amount(decimal value)
        {
            if (value is < 0 or > 1000000)
            {
                throw new InvalidAmountException(value);
            }

            Value = value;
        }

        public static Amount Zero => new(0);
        public static implicit operator Amount(Guid value) => new(value);
        public static implicit operator decimal(Amount id) => id.Value;
    }
}