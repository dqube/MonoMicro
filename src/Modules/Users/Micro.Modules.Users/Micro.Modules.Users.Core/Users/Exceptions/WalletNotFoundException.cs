using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Users.Core.Users.Exceptions
{
    public class WalletNotFoundException : CustomException
    {
        public Guid OwnerId { get; }
        public string Currency { get; }
        public Guid WalletId { get; }

        public WalletNotFoundException(Guid walletId) : base($"Wallet with ID: '{walletId}' was not found.")
        {
            WalletId = walletId;
        }

        public WalletNotFoundException(Guid ownerId, string currency)
            : base($"Wallet for owner with ID: '{ownerId}', currency: '{currency}' was not found.")
        {
            OwnerId = ownerId;
            Currency = currency;
        }
    }
}