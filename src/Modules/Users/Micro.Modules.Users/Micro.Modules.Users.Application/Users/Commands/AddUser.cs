using Micro.Abstractions.Abstractions;
using Micro.Modules.Users.Core.Users.ValueObjects;

namespace Micro.Modules.Users.Application.Users
{
    internal record AddUser(UserId userId, string Name) : ICommand
    {
    }
}