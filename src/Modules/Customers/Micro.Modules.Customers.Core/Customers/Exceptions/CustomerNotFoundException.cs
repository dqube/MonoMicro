using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Customers.Core.Customers.Exceptions;

public class CustomerNotFoundException : CustomException
{
    public int CustomerId { get; }

    public CustomerNotFoundException(int customerId) : base($"Customer with ID: '{customerId}' was not found.")
    {
        CustomerId = customerId;
    }

}