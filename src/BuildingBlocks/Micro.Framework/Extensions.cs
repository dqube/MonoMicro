using Micro.Abstractions;
using Micro.Abstractions.Dispatchers;
using Micro.Abstractions.Modules;
using Micro.API.AsyncApi;
using Micro.API.CORS;
using Micro.API.Exceptions;
using Micro.API.Networking;
using Micro.API.Swagger;
using Micro.Auth;
using Micro.Contexts;
using Micro.HTTP;
using Micro.HTTP.LoadBalancing;
using Micro.HTTP.ServiceDiscovery;
using Micro.Messaging;
using Micro.Messaging.RabbitMQ;
using Micro.Messaging.RabbitMQ.Streams;
using Micro.Observability;
using Micro.Observability.Logging;
using Micro.Security;
using Micro.Security.Vault;
using Micro.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;

namespace Micro.Framework;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
       IList<Assembly> assemblies, IList<IModule> modules)
    {
        var disabledModules = new List<string>();
        using (var serviceProvider = services.BuildServiceProvider())
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
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



        //services.AddMemoryCache();
        //services.AddSingleton<IRequestStorage, RequestStorage>();
        //services.AddAuth(modules);
        //services.AddModuleInfo(modules);
        //services.AddModuleRequests(assemblies);
        //services.AddSingleton<IContextFactory, ContextFactory>();
        //services.AddTransient<IContext>(sp => sp.GetRequiredService<IContextFactory>().Create());
        //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        //services.AddEvents(assemblies);
        //services.AddDomainEvents(assemblies);
        //services.AddCommands(assemblies);
        //services.AddQueries(assemblies);
        //services.AddMessaging();
        //services.AddPostgres();
        //services.AddTransactionalDecorators();
        //services.AddSingleton<IClock, UtcClock>();
        //services.AddHostedService<AppInitializer>(); // Will ApplyMigrations for every known DbContext in solution automatically when application starts

        services.AddControllers()
            .ConfigureApplicationPartManager(manager =>
            {
                // Thanks to this part there will not be any run-time classes from the disabled module
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

            });

        return services;
    }

    public static WebApplicationBuilder AddMicroFramework(this WebApplicationBuilder builder)
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
            .AddObservability(builder.Configuration);

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

    public static WebApplication UseMicroFramework(this WebApplication app)
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