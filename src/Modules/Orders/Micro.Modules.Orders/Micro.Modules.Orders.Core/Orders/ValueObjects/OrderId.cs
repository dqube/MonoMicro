namespace Micro.Modules.Orders.Core.Orders.ValueObjects
{
    internal record struct OrderId
    {
        public int Value { get; }

        public OrderId(int value)
        {
            Value = value;
        }

        public static implicit operator int(OrderId orderId)
            => orderId.Value;

        public static implicit operator OrderId(int value)
            => new(value);
    }
}