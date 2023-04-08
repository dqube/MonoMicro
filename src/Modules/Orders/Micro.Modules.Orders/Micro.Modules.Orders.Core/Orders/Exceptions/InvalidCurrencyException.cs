using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Orders.Core.Orders.Exceptions
{
    public class InvalidCurrencyException : CustomException
    {
        public string Currency { get; }

        public InvalidCurrencyException(string currency) : base($"Currency: '{currency}' is invalid.")
        {
            Currency = currency;
        }
    }
}