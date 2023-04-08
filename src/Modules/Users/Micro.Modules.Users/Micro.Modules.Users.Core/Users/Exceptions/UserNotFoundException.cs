using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Users.Core.Users.Exceptions
{
    public class UserNotFoundException : CustomException
    {
        public int UserId { get; }

        public UserNotFoundException(int userId) : base($"User with ID: '{userId}' was not found.")
        {
            UserId = userId;
        }

    }
}