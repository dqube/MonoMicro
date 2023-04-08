using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Orders.Core.Orders.Exceptions
{
    public class InvalidTransferCurrencyException : CustomException
    {
        public string Currency { get; }

        public InvalidTransferCurrencyException(string currency) : base($"Transfer has invalid currency: '{currency}'.")
        {
            Currency = currency;
        }
    }
}