using Micro.Modules.Customers.Application.Customers.DTO;
using Micro.Modules.Customers.Core.Customers.Entities;

namespace Micro.Modules.Customers.Infrastructure.DAL.Mappings;

internal static class CustomersExtensions
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