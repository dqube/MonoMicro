using Microsoft.AspNetCore.Mvc;

namespace Micro.Modules.Customers.Module.Controllers;
[ApiController]
[Route("api/customers")]

public class CustomersController : Controller
{

    //private readonly ICommandDispatcher _commandDispatcher;
    //private readonly IQueryDispatcher _queryDispatcher;


    //public CustomersController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    //{
    //    _commandDispatcher = commandDispatcher;
    //    _queryDispatcher = queryDispatcher;
    //}
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
