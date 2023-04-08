using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Persons.Core.Persons.Exceptions
{
    public class InvalidAmountException : CustomException
    {
        public decimal Amount { get; }

        public InvalidAmountException(decimal amount) : base($"Amount: '{amount}' is invalid.")
        {
            Amount = amount;
        }
    }
}