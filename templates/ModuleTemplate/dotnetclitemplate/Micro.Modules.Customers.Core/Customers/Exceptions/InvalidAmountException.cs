using BASEREF-NAME.Abstractions.Exceptions;

namespace Micro.Modules.Customers.Core.Customers.Exceptions;

public class InvalidAmountException : CustomException
{
    public decimal Amount { get; }

    public InvalidAmountException(decimal amount) : base($"Amount: '{amount}' is invalid.")
    {
        Amount = amount;
    }
}