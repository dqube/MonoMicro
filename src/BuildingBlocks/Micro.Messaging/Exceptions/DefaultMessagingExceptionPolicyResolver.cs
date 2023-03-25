using Micro.Abstractions.Abstractions;

namespace Micro.Messaging.Exceptions;

internal sealed class DefaultMessagingExceptionPolicyResolver : IMessagingExceptionPolicyResolver
{
    public MessageExceptionPolicy? Resolve(IMessage message, Exception exception) => null;
}