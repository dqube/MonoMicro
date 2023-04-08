using Micro.Abstractions.Kernel.Types;
using Micro.Modules.Orders.Core.Orders.ValueObjects;

namespace Micro.Modules.Orders.Core.Orders.Entities
{
    internal class Order : Aggregate<OrderId>
    {
        private Order(OrderId id, string name) : base(id)
        {
            Name = name;
            Id = id;
        }


        public string Name { get; private set; }
        public OrderId Id { get; private set; }

        public static Order Create(OrderId orderId, string name)
        {
            var order = new Order(orderId, name);
            return order;
        }
    }
}