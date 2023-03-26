using $ext_projectname$.Abstractions.Abstractions;
using $ext_projectname$.Contexts;

namespace $safeprojectname$;

public record MessageEnvelope<T>(T Message, MessageContext Context) where T : IMessage;