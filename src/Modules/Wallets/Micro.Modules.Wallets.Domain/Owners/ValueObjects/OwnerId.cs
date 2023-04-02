using Micro.Abstractions.Kernel.Types;

namespace Micro.Modules.Wallets.Domain.Owners.ValueObjects;

internal record struct OwnerId
{
    public Guid Value { get; }

    public OwnerId(Guid value)
    {
        Value = value;
    }

    public static implicit operator OwnerId(Guid value) => new(value);
    public static implicit operator Guid(OwnerId id) => id.Value;
}