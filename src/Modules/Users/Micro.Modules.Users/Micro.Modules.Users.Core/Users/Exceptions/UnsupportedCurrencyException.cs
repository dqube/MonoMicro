using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Users.Core.Users.Exceptions
{
    public class UnsupportedCurrencyException : CustomException
    {
        public string Currency { get; }

        public UnsupportedCurrencyException(string currency) : base($"Currency: '{currency}' is unsupported.")
        {
            Currency = currency;
        }
    }
}