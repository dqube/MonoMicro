using Micro.Abstractions.Handlers;
using Micro.Contexts;
using Micro.Modules.Wallets.Application.Wallets.DTO;
using Micro.Modules.Wallets.Application.Wallets.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Micro.Modules.Wallets.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
internal class WalletsController : Controller
{
    private const string Policy = "wallets";
    private readonly IDispatcher _dispatcher;
    private readonly IContext _context;

    public WalletsController(IDispatcher dispatcher, IContext context)
    {
        _dispatcher = dispatcher;
        _context = context;
    }

    //[HttpGet]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //public async Task<ActionResult<Paged<WalletDto>>> BrowseAsync([FromQuery] BrowseWallets query)
    //{
    //    if (query.OwnerId.HasValue || _context.Identity.IsUser())
    //    {
    //        // Customer cannot access the other wallets
    //        query.OwnerId = _context.Identity.IsUser() ? _context.Identity.Id : query.OwnerId;
    //    }

    //    return Ok(await _dispatcher.QueryAsync(query));
    //}

    [HttpGet("{walletId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<WalletDetailsDto>> GetAsync(Guid walletId)
    {
        var wallet = await _dispatcher.QueryAsync(new GetWallet { WalletId = walletId });
        if (wallet is not null)
        {
            return Ok(wallet);
        }

        return NotFound();
    }
}