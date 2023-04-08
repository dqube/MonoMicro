using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Persons.Core.Persons.Exceptions
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