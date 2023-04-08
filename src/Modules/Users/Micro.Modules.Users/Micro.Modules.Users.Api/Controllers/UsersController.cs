using Micro.Abstractions.Handlers;
using Micro.Abstractions.Pagination;
using Micro.Modules.Users.Application.Users;
using Micro.Modules.Users.Application.Users.DTO;
using Micro.Modules.Users.Application.Users.Queries;
using Micro.Modules.Users.Core.Users.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Micro.Modules.Users.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class UsersController : Controller
    {
        private const string Policy = "users";
        private readonly IDispatcher _dispatcher;

        public UsersController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Paged<UserDetailsDto>>> BrowseAsync([FromQuery] BrowseUsers query)
            => Ok(await _dispatcher.QueryAsync(query));

        [HttpGet("{userId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<UserDetailsDto>> GetAsync(int userId)
        {
            // User cannot access the other user accounts
            var user = await _dispatcher.QueryAsync(new GetUser { UserId = userId });
            if (user is not null)
            {
                return Ok(user);
            }

            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(AddUser command)
        {
            await _dispatcher.SendAsync(command);
            return NoContent();
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Post(UpdateUser command)
        {
            await _dispatcher.SendAsync(command);
            return NoContent();
        }

    }
}