﻿using $ext_projectname$.Abstractions.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace $safeprojectname$;

public sealed class ModuleSubscriber : IModuleSubscriber
{
    private readonly IModuleRegistry _moduleRegistry;
    private readonly IServiceProvider _serviceProvider;

    public ModuleSubscriber(IModuleRegistry moduleRegistry, IServiceProvider serviceProvider)
    {
        _moduleRegistry = moduleRegistry;
        _serviceProvider = serviceProvider;
    }
        
    public IModuleSubscriber Subscribe<TRequest, TResponse>(string path,
        Func<TRequest, IServiceProvider, CancellationToken, Task<TResponse>> action)
        where TRequest : class where TResponse : class
    {
        _moduleRegistry.AddRequestAction(path, typeof(TRequest), typeof(TResponse),
            async (request, cancellationToken) =>
            {
                using var scope = _serviceProvider.CreateScope();
                return await action((TRequest) request, scope.ServiceProvider, cancellationToken);
            });

        return this;
    }
}