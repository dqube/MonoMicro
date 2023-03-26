using $safeprojectname$.Brokers;

namespace $safeprojectname$.Exceptions;

public record MessageExceptionPolicy(bool Retry, bool UseDeadLetter, Func<IMessageBroker, Task>? Publish = null);