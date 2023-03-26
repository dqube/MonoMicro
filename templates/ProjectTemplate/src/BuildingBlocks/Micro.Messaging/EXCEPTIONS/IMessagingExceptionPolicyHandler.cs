using $ext_projectname$.Abstractions.Abstractions;

namespace $safeprojectname$.Exceptions;

public interface IMessagingExceptionPolicyHandler
{
    Task HandleAsync<T>(T message, Func<Task> handler) where T : IMessage;
}