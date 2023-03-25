using Micro.Abstractions.Abstractions;

namespace Micro.Messaging.Exceptions;

public interface IMessagingExceptionPolicyResolver
{
    MessageExceptionPolicy? Resolve(IMessage message, Exception exception);
}