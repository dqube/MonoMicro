namespace Micro.Modules.Customers.Core.Customers.ValueObjects;

internal record struct CustomerId
{
    public int Value { get; }

    public CustomerId(int value)
    {
        Value = value;
    }

    public static implicit operator int(CustomerId customerId)
        => customerId.Value;

    public static implicit operator CustomerId(int value)
        => new(value);
}
