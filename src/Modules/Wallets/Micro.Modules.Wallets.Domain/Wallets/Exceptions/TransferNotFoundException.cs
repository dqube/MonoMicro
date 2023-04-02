using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Wallets.Domain.Wallets.Exceptions;

public class TransferNotFoundException : CustomException
{
    public Guid TransferId { get; }

    public TransferNotFoundException(Guid transferId) : base($"Transfer with ID: '{transferId}' was not found.")
    {
        TransferId = transferId;
    }
}