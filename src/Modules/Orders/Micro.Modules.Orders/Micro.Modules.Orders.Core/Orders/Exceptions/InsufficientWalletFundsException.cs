using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Orders.Core.Orders.Exceptions
{
    internal class InsufficientWalletFundsException : CustomException
    {
        public Guid WalletId { get; }

        public InsufficientWalletFundsException(Guid walletId)
            : base($"Insufficient funds for wallet with ID: '{walletId}'.")
        {
            WalletId = walletId;
        }
    }
}