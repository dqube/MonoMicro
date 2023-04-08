using Micro.Abstractions.Abstractions;
using Micro.Modules.Users.Core.Users.ValueObjects;

namespace Micro.Modules.Users.Application.Users
{
    internal record UpdateUser(int userId, string Name) : ICommand
    {
    }
}