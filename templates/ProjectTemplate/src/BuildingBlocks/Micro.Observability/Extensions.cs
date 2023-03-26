using $safeprojectname$.Metrics;
using $safeprojectname$.Tracing;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace $safeprojectname$;

public static class Extensions
{
    public static IServiceCollection AddObservability(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOpenTelemetry()
            .AddMetrics(services, configuration)
            .AddTracing(services, configuration);

        return services;
    }

    public static IApplicationBuilder UseObservability(this IApplicationBuilder app)
        => app.UseMetrics();
}