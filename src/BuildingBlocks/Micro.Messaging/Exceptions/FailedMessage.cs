using Micro.Abstractions.Abstractions;

namespace Micro.Messaging.Exceptions;

public record FailedMessage(IMessage Message);