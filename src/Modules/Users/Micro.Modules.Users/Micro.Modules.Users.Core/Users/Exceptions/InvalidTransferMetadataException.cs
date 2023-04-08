using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Users.Core.Users.Exceptions
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