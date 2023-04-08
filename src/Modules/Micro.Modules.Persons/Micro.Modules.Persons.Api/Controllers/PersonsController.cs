using Micro.Abstractions.Handlers;
using Micro.Abstractions.Pagination;
using Micro.Modules.Persons.Application.Persons;
using Micro.Modules.Persons.Application.Persons.DTO;
using Micro.Modules.Persons.Application.Persons.Queries;
using Micro.Modules.Persons.Core.Persons.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Micro.Modules.Persons.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class PersonsController : Controller
    {
        private const string Policy = "persons";
        private readonly IDispatcher _dispatcher;

        public PersonsController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Paged<PersonDetailsDto>>> BrowseAsync([FromQuery] BrowsePersons query)
            => Ok(await _dispatcher.QueryAsync(query));

        [HttpGet("{personId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<PersonDetailsDto>> GetAsync(int personId)
        {
            // Person cannot access the other person accounts
            var person = await _dispatcher.QueryAsync(new GetPerson { PersonId = personId });
            if (person is not null)
            {
                return Ok(person);
            }

            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(AddPerson command)
        {
            await _dispatcher.SendAsync(command);
            return NoContent();
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Post(UpdatePerson command)
        {
            await _dispatcher.SendAsync(command);
            return NoContent();
        }

    }
}