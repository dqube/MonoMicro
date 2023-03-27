namespace Micro.Modules.Customers.Domain.Entities;

public class Customer
{
    public Customer(
        int id,
        string name,
        string email,
        string firstName,
        string lastName
    )
    {
        Id = id;
        Name = name;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }
    public int Id { get; private set; }

    public string? Name { get; private set; }

    public string? Email { get; private set; }

    public string? FirstName { get; private set; }

    public string? LastName { get; private set; }
    public static Customer Create(
    int id,
    string name,
    string email,
    string firstName,
    string lastName
     )
    {
        var customer = new Customer(
         id,
         name,
         email,
         firstName,
         lastName
         );
        return customer;
    }
}
