using $safeprojectname$.Accessors;
using $safeprojectname$.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace $safeprojectname$;

public static class Extensions
{
    public static IServiceCollection AddContexts(this IServiceCollection services)
    {
        services.AddSingleton<IContextProvider, ContextProvider>();
        services.AddSingleton<IContextAccessor, ContextAccessor>();

        return services;
    }
}