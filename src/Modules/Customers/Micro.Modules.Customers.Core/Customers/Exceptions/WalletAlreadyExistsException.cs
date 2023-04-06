using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Customers.Core.Customers.Exceptions;

public class WalletAlreadyExistsException : CustomException
{
    public Guid OwnerId { get; }
    public string Currency { get; }

    public WalletAlreadyExistsException(Guid ownerId, string currency)
        : base($"Wallet for owner with ID: '{ownerId}', currency: '{currency}' already exists.")
    {
        OwnerId = ownerId;
        Currency = currency;
    }
}