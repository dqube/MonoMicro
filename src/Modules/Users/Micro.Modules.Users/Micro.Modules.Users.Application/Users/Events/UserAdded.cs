using Micro.Abstractions.Abstractions;

namespace Micro.Modules.Users.Application.Users.Events
{
    internal record UserAdded(int UserId, string Name) : IEvent;
}