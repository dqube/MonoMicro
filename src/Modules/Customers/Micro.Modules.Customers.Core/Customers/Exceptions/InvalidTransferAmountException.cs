using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Customers.Core.Customers.Exceptions;

public class InvalidTransferAmountException : CustomException
{
    public decimal Amount { get; }

    public InvalidTransferAmountException(decimal amount) : base($"Transfer has invalid amount: '{amount}'.")
    {
        Amount = amount;
    }
}