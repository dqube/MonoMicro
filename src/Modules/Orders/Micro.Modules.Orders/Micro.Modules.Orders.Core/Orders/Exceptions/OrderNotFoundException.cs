using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Orders.Core.Orders.Exceptions
{
    public class OrderNotFoundException : CustomException
    {
        public int OrderId { get; }

        public OrderNotFoundException(int orderId) : base($"Order with ID: '{orderId}' was not found.")
        {
            OrderId = orderId;
        }

    }
}