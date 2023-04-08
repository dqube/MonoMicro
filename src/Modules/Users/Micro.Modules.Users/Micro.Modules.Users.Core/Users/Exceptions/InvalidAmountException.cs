using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Users.Core.Users.Exceptions
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