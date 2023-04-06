using Micro.Modules.Customers.Application.Customers.DTO;
using Micro.Modules.Customers.Core.Customers.Entities;

namespace Micro.Modules.Customers.Core.Queries.Handlers;

internal static class Extensions
{
    public static CustomerDto AsDto(this Customer customer)
        => customer.Map<CustomerDto>();

    public static CustomerDetailsDto AsDetailsDto(this Customer customer)
    {
        var dto = customer.Map<CustomerDetailsDto>();
        

        return dto;
    }

    private static T Map<T>(this Customer customer) where T : CustomerDto, new()
        => new()
        {
            CustomerId = customer.Id,
            Name = customer.Name,            
           // CreatedAt = customer.CreatedAt
        };

   
}