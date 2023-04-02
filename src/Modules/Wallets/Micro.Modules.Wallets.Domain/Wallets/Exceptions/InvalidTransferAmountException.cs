using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Wallets.Domain.Wallets.Exceptions;

public class InvalidTransferAmountException : CustomException
{
    public decimal Amount { get; }

    public InvalidTransferAmountException(decimal amount) : base($"Transfer has invalid amount: '{amount}'.")
    {
        Amount = amount;
    }
}