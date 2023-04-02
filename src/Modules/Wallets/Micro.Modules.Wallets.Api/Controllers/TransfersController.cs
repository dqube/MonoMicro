using Micro.Modules.Wallets.Application.Wallets.Queries;
using Micro.Abstractions.Handlers;
using Micro.Abstractions.Pagination;
using Micro.Contexts;
using Micro.Modules.Wallets.Application.Wallets.Commands;
using Micro.Modules.Wallets.Application.Wallets.DTO;
using Micro.Modules.Wallets.Application.Wallets.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Micro.Modules.Wallets.Api.Controllers;

[ApiController]
//[Route("[controller]")]
[Route("api/transfers")]

//[Authorize]
internal class TransfersController : Controller
{
    private const string Policy = "transfers";
    private readonly IDispatcher _dispatcher;

    public TransfersController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
      //  _context = context;
    }

   // [HttpGet]
   //// [Authorize(Policy)]
   // [ProducesResponseType(StatusCodes.Status200OK)]
   // //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
   // public async Task<ActionResult<Paged<TransferDto>>> BrowseAsync([FromQuery] BrowseTransfers query)
   //     => Ok(await _dispatcher.QueryAsync(query));

    [HttpGet]
   // [Authorize(Policy)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<TransferDetailsDto>> GetAsync()
    
    {
        var transfer = await _dispatcher.QueryAsync(new GetTransfer { TransferId = new Guid() });
        if (transfer is not null)
        {
            return Ok(transfer);
        }

        return NotFound();
    }

    //[HttpPost("funds")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //public async Task<ActionResult> Post(TransferFunds command)
    //{
    //    await _dispatcher.SendAsync(command.Bind(x => x.OwnerId, _context.Identity.Id));
    //    return NoContent();
    //}

   // [Authorize(Policy)]
    [HttpPost("incoming")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Post(AddFunds command)
    {
        await _dispatcher.SendAsync(command);
        return NoContent();
    }

   // [Authorize(Policy)]
    [HttpPost("outgoing")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Post(DeductFunds command)
    {
        await _dispatcher.SendAsync(command);
        return NoContent();
    }
}