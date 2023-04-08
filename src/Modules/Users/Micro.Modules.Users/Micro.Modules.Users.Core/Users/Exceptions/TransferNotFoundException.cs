using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Users.Core.Users.Exceptions
{
    public class TransferNotFoundException : CustomException
    {
        public Guid TransferId { get; }

        public TransferNotFoundException(Guid transferId) : base($"Transfer with ID: '{transferId}' was not found.")
        {
            TransferId = transferId;
        }
    }
}