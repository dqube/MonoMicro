using BASEREF-NAME.Abstractions.Abstractions;
using Micro.Modules.Customers.Core.Customers.ValueObjects;

namespace Micro.Modules.Customers.Application.Customers;

internal record AddCustomer(CustomerId customerId, string Name) : ICommand
{
}
