using Micro.Abstractions.Kernel.Types;
using Micro.Modules.Customers.Core.Customers.ValueObjects;

namespace Micro.Modules.Customers.Core.Customers.Entities;

internal class Customer : Aggregate<CustomerId>
{
    private Customer()
    {

    }
    private Customer(CustomerId customerId, string name) : base(customerId)
    {
        Name = name;
       // Id= id;
    }
    

    public string Name { get; private set; }
   // public CustomerId Id { get; private set; }

    public static Customer Create(CustomerId customerId, string name)
    {
        var customer = new Customer(customerId,name);
        return customer;
    }
}
