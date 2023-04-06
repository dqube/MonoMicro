using Micro.Abstractions.Abstractions;
using Micro.Modules.Customers.Core.Customers.ValueObjects;

namespace Micro.Modules.Customers.Application.Customers;

internal record UpdateCustomer(CustomerId customerId,string Name) : ICommand
{
}
