using Micro.Abstractions.Abstractions;
using Micro.Modules.Users.Application.Users.DTO;

namespace Micro.Modules.Users.Application.Users.Queries
{
    internal class GetUser : IQuery<UserDetailsDto>
    {
        public int UserId { get; set; }
    }
}