using $safeprojectname$.Brokers;
using $safeprojectname$.Clients;
using $safeprojectname$.Exceptions;
using $safeprojectname$.Streams;
using $safeprojectname$.Streams.Serialization;
using $safeprojectname$.Subscribers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace $safeprojectname$;

public static class Extensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("messaging");
        services.Configure<MessagingOptions>(section);
        services.AddTransient<IMessageBroker, MessageBroker>();
        services.AddSingleton<IMessageBrokerClient, DefaultMessageBrokerClient>();
        services.AddSingleton<IMessageSubscriber, DefaultMessageSubscriber>();
        services.AddSingleton<IMessagingExceptionPolicyResolver, DefaultMessagingExceptionPolicyResolver>();
        services.AddSingleton<IMessagingExceptionPolicyHandler, DefaultMessagingExceptionPolicyHandler>();
        services.AddSingleton<IStreamSerializer, SystemTextJsonStreamSerializer>();
        services.AddSingleton<IStreamPublisher, DefaultStreamPublisher>();
        services.AddSingleton<IStreamSubscriber, DefaultStreamSubscriber>();
        
        return services;
    }
    
    public static IMessageSubscriber Subscribe(this IApplicationBuilder app)
        => app.ApplicationServices.GetRequiredService<IMessageSubscriber>();
}