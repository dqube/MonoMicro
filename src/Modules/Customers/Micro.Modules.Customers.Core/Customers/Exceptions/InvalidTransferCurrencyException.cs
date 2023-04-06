using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Customers.Core.Customers.Exceptions;

public class InvalidTransferCurrencyException : CustomException
{
    public string Currency { get; }

    public InvalidTransferCurrencyException(string currency) : base($"Transfer has invalid currency: '{currency}'.")
    {
        Currency = currency;
    }
}