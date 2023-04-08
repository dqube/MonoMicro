using Micro.Modules.Users.Application.Users.DTO;
using Micro.Modules.Users.Core.Users.Entities;

namespace Micro.Modules.Users.Infrastructure.DAL.Mappings
{
    internal static class UsersExtensions
    {
        public static UserDto AsDto(this User user)
            => user.Map<UserDto>();

        public static UserDetailsDto AsDetailsDto(this User user)
        {
            if (user is null) return new UserDetailsDto();
            var dto = user.Map<UserDetailsDto>();

            return dto;
        }

        private static T Map<T>(this User user) where T : UserDto, new()
            => new()
            {
                UserId = user.Id,
                Name = user.Name,
                // CreatedAt = user.CreatedAt
            };

    }
}