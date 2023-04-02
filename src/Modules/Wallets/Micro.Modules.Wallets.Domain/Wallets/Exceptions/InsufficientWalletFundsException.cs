using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Wallets.Domain.Wallets.Exceptions;

internal class InsufficientWalletFundsException : CustomException
{
    public Guid WalletId { get; }

    public InsufficientWalletFundsException(Guid walletId)
        : base($"Insufficient funds for wallet with ID: '{walletId}'.")
    {
        WalletId = walletId;
    }
}