namespace $safeprojectname$.ServiceDiscovery;

public interface IServiceDiscoveryRegistration
{
    IEnumerable<string> Tags { get; }
}