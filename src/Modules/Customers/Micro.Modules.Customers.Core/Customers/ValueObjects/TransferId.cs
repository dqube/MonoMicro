using Micro.Abstractions.Kernel.Types;
using Micro.Modules.Customers.Core.Customers.ValueObjects;

namespace Micro.Modules.Customers.Core.Customers.ValueObjects;


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