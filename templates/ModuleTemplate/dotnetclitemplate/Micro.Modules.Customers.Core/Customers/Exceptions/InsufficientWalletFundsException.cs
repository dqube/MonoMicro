using BASEREF-NAME.Abstractions.Exceptions;

namespace Micro.Modules.Customers.Core.Customers.Exceptions;

internal class InsufficientWalletFundsException : CustomException
{
    public Guid WalletId { get; }

    public InsufficientWalletFundsException(Guid walletId)
        : base($"Insufficient funds for wallet with ID: '{walletId}'.")
    {
        WalletId = walletId;
    }
}