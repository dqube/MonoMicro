using Micro.Abstractions.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Micro.Modules.Customers.Module.Controllers;
[ApiController]
[Route("api/customers")]

public class CustomersController : Controller
{

    private readonly ICommandDispatcher _commanddispatcher;
    private readonly IQueryDispatcher _querydispatcher;


    public CustomersController(ICommandDispatcher commanddispatcher, IQueryDispatcher querydispatcher)
    {
        _commanddispatcher = commanddispatcher;
        _querydispatcher = querydispatcher;
    }
    [HttpGet]
    public IActionResult ListDinners()
    {
        return Ok(Array.Empty<string>());
    }
    //[HttpGet("tracks/{id:guid}")]
    //[AllowAnonymous]
    //public async Task<ActionResult<AgendaTrackDto>> GetAgendaTrackAsync(Guid id)
    //        => OkOrNotFound(await _queryDispatcher.QueryAsync(new GetAgendaTrack { Id = id }));

}
