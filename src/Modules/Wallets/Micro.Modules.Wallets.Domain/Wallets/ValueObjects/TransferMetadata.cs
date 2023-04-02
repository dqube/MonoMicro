using Micro.Abstractions.Kernel.Types;
using Micro.Modules.Wallets.Domain.Wallets.Exceptions;

namespace Micro.Modules.Wallets.Domain.Wallets.ValueObjects;

internal record TransferMetadata 
{
    public string Value { get; }

    public TransferMetadata(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        if (value.Length > 1000)
        {
            throw new InvalidTransferMetadataException(value);
        }

        Value = value.Trim();
    }
    public static implicit operator TransferMetadata(string value) => new(value);
    public static implicit operator string(TransferMetadata id) => id.Value;
}