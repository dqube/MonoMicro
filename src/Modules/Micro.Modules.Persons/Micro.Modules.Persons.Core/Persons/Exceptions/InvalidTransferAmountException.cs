using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Persons.Core.Persons.Exceptions
{
    public class InvalidTransferAmountException : CustomException
    {
        public decimal Amount { get; }

        public InvalidTransferAmountException(decimal amount) : base($"Transfer has invalid amount: '{amount}'.")
        {
            Amount = amount;
        }
    }
}