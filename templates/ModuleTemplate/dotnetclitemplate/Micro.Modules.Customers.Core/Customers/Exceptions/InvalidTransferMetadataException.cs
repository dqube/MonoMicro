using BASEREF-NAME.Abstractions.Exceptions;

namespace Micro.Modules.Customers.Core.Customers.Exceptions;

internal class InvalidTransferMetadataException : CustomException
{
    public string Metadata { get; }

    public InvalidTransferMetadataException(string metadata) : base($"Transfer metadata: '{metadata}' is invalid.")
    {
        Metadata = metadata;
    }
}