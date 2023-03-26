namespace $safeprojectname$.ServiceDiscovery;

internal class DefaultServiceDiscoveryRegistration : IServiceDiscoveryRegistration
{
    public IEnumerable<string> Tags { get; } = Enumerable.Empty<string>();
}