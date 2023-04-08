using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Orders.Core.Orders.Exceptions
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