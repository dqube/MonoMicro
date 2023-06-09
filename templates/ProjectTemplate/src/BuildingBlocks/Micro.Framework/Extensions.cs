﻿using $ext_projectname$.Abstractions;
using $ext_projectname$.Abstractions.Dispatchers;
using $ext_projectname$.API;
using $ext_projectname$.API.AsyncApi;
using $ext_projectname$.API.CORS;
using $ext_projectname$.API.Exceptions;
using $ext_projectname$.API.Networking;
using $ext_projectname$.API.Swagger;
using $ext_projectname$.Auth;
using $ext_projectname$.Contexts;
using $ext_projectname$.Contracts;
using $ext_projectname$.DAL.SqlServer;
using $ext_projectname$.HTTP;
using $ext_projectname$.HTTP.LoadBalancing;
using $ext_projectname$.HTTP.ServiceDiscovery;
using $ext_projectname$.Messaging;
using $ext_projectname$.Messaging.RabbitMQ;
using $ext_projectname$.Messaging.RabbitMQ.Streams;
using $ext_projectname$.Modules;
using $ext_projectname$.Observability;
using $ext_projectname$.Observability.Logging;
using $ext_projectname$.Security;
using $ext_projectname$.Security.Vault;
using $ext_projectname$.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace $safeprojectname$;

public static class Extensions
{
    
    public static WebApplicationBuilder AddModularFramework(this WebApplicationBuilder builder)
    {
        builder.ConfigureModules();
        var configuration = builder.Configuration;
        var modulePart = configuration.BindOptions<AppOptions>("app").ModulePart;
        var _assemblies = ModuleLoader.LoadAssemblies(configuration, modulePart);
        var _modules = ModuleLoader.LoadModules(_assemblies);
        var disabledModules = new List<string>();
        using (var serviceProvider = builder.Services.BuildServiceProvider())
        {
            foreach (var (key, value) in configuration.AsEnumerable())
            {
                if (!key.Contains(":module:enabled"))
                {
                    continue;
                }

                if (!bool.Parse(value))
                {
                    disabledModules.Add(key.Split(":")[0]);
                }
            }
        }
        builder.Services
            .AddSqlServerModule();

        foreach (var module in _modules)
        {
            module.Register(builder.Services);
        }
        //builder.AddFramework();
        builder.AddVault();

        var appOptions = builder.Configuration.GetSection("app").BindOptions<AppOptions>();
        var appInfo = new AppInfo(appOptions.Name, appOptions.Version);
        builder.Services.AddSingleton(appInfo);

        RenderLogo(appOptions);

        builder
            .AddLogging()
            .Services
            .AddErrorHandling()
        .AddModuleInfo(_modules)
        .AddModuleRequests(_assemblies)
         .AddModuleHandlers(_assemblies)
            .AddDispatchers()
            .AddContexts()
            .AddMemoryCache()
            .AddHttpContextAccessor()
            .AddMicro(builder.Configuration)
            //.AddAuth(builder.Configuration)
            .AddCorsPolicy(builder.Configuration)
            .AddSwaggerDocs(builder.Configuration)
            .AddStorage(builder.Configuration)
            .AddAsyncApiDocs(builder.Configuration)
            .AddHeadersForwarding(builder.Configuration)
            .AddMessaging(builder.Configuration)
            .AddRabbitMQ(builder.Configuration)
            .AddRabbitMQStreams(builder.Configuration)
            .AddConsul(builder.Configuration)
            .AddFabio(builder.Configuration)
            .AddSecurity(builder.Configuration)
            .AddLogger(builder.Configuration)
            .AddObservability(builder.Configuration)
            .AddContracts();

        builder.Services
           .AddHttpClient(builder.Configuration)
           .AddVaultCertificatesHandler(builder.Configuration);
        builder.Services.AddControllers()
            .ConfigureApplicationPartManager(manager =>
            {
                var removedParts = new List<ApplicationPart>();
                foreach (var disabledModule in disabledModules)
                {
                    var parts = manager.ApplicationParts.Where(x => x.Name.Contains(disabledModule,
                        StringComparison.InvariantCultureIgnoreCase));
                    removedParts.AddRange(parts);
                }

                foreach (var part in removedParts)
                {
                    manager.ApplicationParts.Remove(part);
                }

                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });

        _assemblies.Clear();
        _modules.Clear();
        return builder;
    }

    public static WebApplicationBuilder AddFramework(this WebApplicationBuilder builder)
    {
        
        builder.AddVault();

        var appOptions = builder.Configuration.GetSection("app").BindOptions<AppOptions>();
        var appInfo = new AppInfo(appOptions.Name, appOptions.Version);
        builder.Services.AddSingleton(appInfo);
        
        RenderLogo(appOptions);

        builder
            .AddLogging()
            .Services
            .AddErrorHandling()
            .AddHandlers(appOptions.Project)
            .AddDispatchers()
            .AddContexts()
            .AddMemoryCache()
            .AddHttpContextAccessor()
            .AddMicro(builder.Configuration)
            .AddAuth(builder.Configuration)
            .AddCorsPolicy(builder.Configuration)
            .AddSwaggerDocs(builder.Configuration)
            .AddStorage(builder.Configuration)
            .AddAsyncApiDocs(builder.Configuration)
            .AddHeadersForwarding(builder.Configuration)
            .AddMessaging(builder.Configuration)
            .AddRabbitMQ(builder.Configuration)
            .AddRabbitMQStreams(builder.Configuration)
            .AddConsul(builder.Configuration)
            .AddFabio(builder.Configuration)
            .AddSecurity(builder.Configuration)
            .AddLogger(builder.Configuration)
            .AddObservability(builder.Configuration)
            .AddContracts();

        builder.Services
            .AddHttpClient(builder.Configuration)
            .AddVaultCertificatesHandler(builder.Configuration);
        // .AddConsulHandler()
        // .AddFabioHandler();

        // builder.Services
        //     .AddMessagingMetricsDecorators()
        //     .AddMessagingTracingDecorators();

        return builder;
    }
    public static WebApplication UseModularFramework(this WebApplication app)
    {
        app.UseFramework();
        var configuration = app.Configuration;
        var options=configuration.BindOptions<AppOptions>("app");
        var modulePart = options.ModulePart;
        var _assemblies = ModuleLoader.LoadAssemblies(configuration, modulePart);
        var _modules = ModuleLoader.LoadModules(_assemblies);
        foreach (var module in _modules)
        {
            module.Use(app);
        }
        app.ValidateContracts(_assemblies);
#pragma warning disable ASP0014 // Suggest using top level route registrations
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", context => context.Response.WriteAsync(options.Name));
            endpoints.MapModuleInfo();
        });
#pragma warning restore ASP0014 // Suggest using top level route registrations

        _assemblies.Clear();
        _modules.Clear();
        return app;
    }
    public static WebApplication UseFramework(this WebApplication app)
    {
        app
            .UseHeadersForwarding()
            .UseCorsPolicy()
            .UseErrorHandling()
            .UseSwaggerDocs()
            .UseAuthentication()
            .UseRouting()
            .UseObservability()
            .UseAuthorization()
            .UseContextLogger()
            .UseSerilogRequestLogging()
            .UseEndpoints(endpoints => endpoints.MapAsyncApiDocs(app.Configuration));

        return app;
    }

    private static void RenderLogo(AppOptions app)
    {
        if (string.IsNullOrWhiteSpace(app.Name))
        {
            return;
        }

        Console.WriteLine(Figgle.FiggleFonts.Slant.Render($"{app.Name} {app.Version}"));
    }

}