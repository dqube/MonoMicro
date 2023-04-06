namespace Micro.Abstractions.Abstractions;

// Marker interface
public interface ICommand : IMessage
{
}

public interface ICommand<TResponse> : IMessage
{
}