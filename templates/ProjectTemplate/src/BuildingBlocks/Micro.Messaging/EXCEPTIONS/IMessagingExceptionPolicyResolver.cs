using $ext_projectname$.Abstractions.Abstractions;

namespace $safeprojectname$.Exceptions;

public interface IMessagingExceptionPolicyResolver
{
    MessageExceptionPolicy? Resolve(IMessage message, Exception exception);
}