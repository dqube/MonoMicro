using Micro.Abstractions.Handlers;
using Micro.Abstractions.Pagination;
using Micro.Modules.Customers.Application.Customers;
using Micro.Modules.Customers.Application.Customers.DTO;
using Micro.Modules.Customers.Application.Customers.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Micro.Modules.Customers.Api.Controllers;

[ApiController]
[Route("[controller]")]
internal class CustomersController : Controller
{
    private const string Policy = "customers";
    private readonly IDispatcher _dispatcher;

    public CustomersController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }
        
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Paged<CustomerDetailsDto>>> BrowseAsync([FromQuery] BrowseCustomers query)
        => Ok(await _dispatcher.QueryAsync(query));

    [HttpGet("{customerId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<CustomerDetailsDto>> GetAsync(int customerId)
    {
        // Customer cannot access the other customer accounts
        var customer = await _dispatcher.QueryAsync(new GetCustomer { CustomerId = customerId });
        if (customer is not null)
        {
            return Ok(customer);
        }

        return NotFound();
    }
        
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(AddCustomer command)
    {
        await _dispatcher.SendAsync(command);
        return NoContent();
    }

    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Post(UpdateCustomer command)
    {
        await _dispatcher.SendAsync(command);
        return NoContent();
    }
        
  
}