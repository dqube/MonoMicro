using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Orders.Core.Orders.Exceptions
{
    internal class InvalidTransferMetadataException : CustomException
    {
        public string Metadata { get; }

        public InvalidTransferMetadataException(string metadata) : base($"Transfer metadata: '{metadata}' is invalid.")
        {
            Metadata = metadata;
        }
    }
}