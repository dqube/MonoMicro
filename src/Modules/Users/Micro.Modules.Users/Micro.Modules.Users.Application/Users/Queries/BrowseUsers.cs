using Micro.Abstractions.Pagination;
using Micro.Modules.Users.Application.Users.DTO;

namespace Micro.Modules.Users.Application.Users.Queries
{
    internal class BrowseUsers : PagedQuery<UserDto>
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
    }
}