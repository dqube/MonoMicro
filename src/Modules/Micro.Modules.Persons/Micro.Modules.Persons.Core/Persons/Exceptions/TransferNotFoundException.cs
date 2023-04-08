using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Persons.Core.Persons.Exceptions
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