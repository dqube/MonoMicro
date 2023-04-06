using System.Collections.Generic;

namespace Micro.Modules.Customers.Application.Customers.DTO;

internal class CustomerDetailsDto : CustomerDto
{
    public decimal Amount { get; set; }
}