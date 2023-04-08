using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Users.Core.Users.Exceptions
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