using $ext_projectname$.Abstractions.Abstractions;

namespace $safeprojectname$.Exceptions;

internal sealed class DefaultMessagingExceptionPolicyResolver : IMessagingExceptionPolicyResolver
{
    public MessageExceptionPolicy? Resolve(IMessage message, Exception exception) => null;
}