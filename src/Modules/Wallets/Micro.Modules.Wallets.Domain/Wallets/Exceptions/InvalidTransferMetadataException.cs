using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Wallets.Domain.Wallets.Exceptions;

internal class InvalidTransferMetadataException : CustomException
{
    public string Metadata { get; }

    public InvalidTransferMetadataException(string metadata) : base($"Transfer metadata: '{metadata}' is invalid.")
    {
        Metadata = metadata;
    }
}