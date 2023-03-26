using $ext_projectname$.Abstractions.Exceptions;

namespace $safeprojectname$.ServiceDiscovery;

internal sealed class ServiceNotFoundException : CustomException
{
    public string Service { get; }

    public ServiceNotFoundException(string service) : base($"Service: '{service}' was not found.")
    {
        Service = service;
    }
}