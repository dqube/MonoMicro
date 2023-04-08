using BASEREF-NAME.Abstractions.Exceptions;

namespace Micro.Modules.Customers.Core.Customers.Exceptions;

public class UnsupportedCurrencyException : CustomException
{
    public string Currency { get; }

    public UnsupportedCurrencyException(string currency) : base($"Currency: '{currency}' is unsupported.")
    {
        Currency = currency;
    }
}