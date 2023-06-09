﻿using $ext_projectname$.Abstractions.Abstractions;
using $ext_projectname$.Abstractions.Handlers;
using $ext_projectname$.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace $safeprojectname$;

public static class Extensions
{
    public static IServiceCollection AddModuleInfo(this IServiceCollection services, IList<IModule> modules)
    {
        var moduleInfoProvider = new ModuleInfoProvider();
        var moduleInfo =
            modules?.Select(x => new ModuleInfo(x.Name, x.Policies ?? Enumerable.Empty<string>())) ??
            Enumerable.Empty<ModuleInfo>();
        moduleInfoProvider.Modules.AddRange(moduleInfo);
        services.AddSingleton(moduleInfoProvider);

        return services;
    }

    public static void MapModuleInfo(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("modules", context =>
        {
            var moduleInfoProvider = context.RequestServices.GetRequiredService<ModuleInfoProvider>();
            return context.Response.WriteAsJsonAsync(moduleInfoProvider.Modules);
        });
    }

    public static WebApplicationBuilder ConfigureModules(this WebApplicationBuilder builder)
    {
         builder.Host.ConfigureAppConfiguration((ctx, cfg) =>
            {
                foreach (var settings in GetSettings("*"))
                {
                    cfg.AddJsonFile(settings);
                }

                foreach (var settings in GetSettings($"*.{ctx.HostingEnvironment.EnvironmentName}"))
                {
                    cfg.AddJsonFile(settings);
                }

                IEnumerable<string> GetSettings(string pattern)
                    => Directory.EnumerateFiles(ctx.HostingEnvironment.ContentRootPath,
                        $"module.{pattern}.json", SearchOption.AllDirectories);
            });
        return builder;
    }

    public static IServiceCollection AddModuleRequests(this IServiceCollection services, IList<Assembly> assemblies)
    {
        services.AddModuleRegistry(assemblies);
        services.AddSingleton<IModuleClient, ModuleClient>();
         services.AddSingleton<IModuleSerializer, JsonModuleSerializer>();
        //services.AddSingleton<IModuleSerializer, MessagePackModuleSerializer>();
        services.AddSingleton<IModuleSubscriber, ModuleSubscriber>();

        return services;
    }

    public static IModuleSubscriber UseModuleRequests(this IApplicationBuilder app)
        => app.ApplicationServices.GetRequiredService<IModuleSubscriber>();

    private static void AddModuleRegistry(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        var registry = new ModuleRegistry();
        var types = assemblies.SelectMany(x => x.GetTypes()).ToArray();
            
        var commandTypes = types
            .Where(t => t.IsClass && typeof(ICommand).IsAssignableFrom(t))
            .ToArray();
            
        var eventTypes = types
            .Where(x => x.IsClass && typeof(IEvent).IsAssignableFrom(x))
            .ToArray();

        services.AddSingleton<IModuleRegistry>(sp =>
        {
            var commandDispatcher = sp.GetRequiredService<ICommandDispatcher>();
            var commandDispatcherType = commandDispatcher.GetType();
                
            var eventDispatcher = sp.GetRequiredService<IEventDispatcher>();
            var eventDispatcherType = eventDispatcher.GetType();

            foreach (var type in commandTypes)
            {
#pragma warning disable CS8603 // Possible null reference return.
                registry.AddBroadcastAction(type, (@event, cancellationToken) =>
                    (Task)commandDispatcherType.GetMethod(nameof(commandDispatcher.SendAsync))
                        ?.MakeGenericMethod(type)
                        .Invoke(commandDispatcher, new[] {@event, cancellationToken}));
#pragma warning restore CS8603 // Possible null reference return.
            }
                
            foreach (var type in eventTypes)
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                registry.AddBroadcastAction(type, (@event, cancellationToken) =>
                    (Task) eventDispatcherType.GetMethod(nameof(eventDispatcher.PublishAsync))
                        ?.MakeGenericMethod(type)
                        .Invoke(eventDispatcher, new[] {@event, cancellationToken}));
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            }

            return registry;
        });
    }
    public static string GetModuleName(this object value)
        => value?.GetType().GetModuleName() ?? string.Empty;

    public static string GetModuleName(this Type type, string namespacePart = "Modules", int splitIndex = 2)
    {
        if (type?.Namespace is null)
        {
            return string.Empty;
        }

        return type.Namespace.Contains(namespacePart)
            ? type.Namespace.Split(".")[splitIndex].ToLowerInvariant()
            : string.Empty;
    }
}