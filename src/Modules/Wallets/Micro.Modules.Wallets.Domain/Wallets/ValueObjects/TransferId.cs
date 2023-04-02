using Micro.Abstractions.Kernel.Types;
using Micro.Modules.Wallets.Domain.Owners.ValueObjects;

namespace Micro.Modules.Wallets.Domain.Wallets.ValueObjects;


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