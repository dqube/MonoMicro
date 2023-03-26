using $ext_projectname$.Abstractions;
using $ext_projectname$.Abstractions.Storage;
using $safeprojectname$.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace $safeprojectname$;

public static class Extensions
{

    public static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("app");
        var options = section.BindOptions<AppOptions>();
        services.Configure<AppOptions>(section);

        return services
            .AddSingleton<IRequestStorage, RequestStorage>();
    }
}