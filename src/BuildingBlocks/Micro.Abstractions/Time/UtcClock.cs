namespace Micro.Abstractions.Time;

public sealed class UtcClock : IClock
{
    public DateTime Current() => DateTime.UtcNow;
}