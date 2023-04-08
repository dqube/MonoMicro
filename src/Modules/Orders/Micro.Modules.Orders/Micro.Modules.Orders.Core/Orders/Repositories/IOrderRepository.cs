using Micro.Modules.Orders.Core.Orders.Entities;
using Micro.Modules.Orders.Core.Orders.ValueObjects;

namespace Micro.Modules.Orders.Core.Orders.Repositories
{
    internal interface IOrderRepository
    {
        Task<Order> GetAsync(OrderId id);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
    }
}