using $ext_projectname$.Abstractions.Abstractions;

namespace $safeprojectname$.Streams.Serialization;

public interface IStreamSerializer
{
    byte[] Serialize<T>(T message) where T : IMessage;
    T? Deserialize<T>(byte[] bytes) where T : IMessage;
}