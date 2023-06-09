using $safeprojectname$.Identity;
using $safeprojectname$.Initializer;
using $safeprojectname$.Serialization;
using $safeprojectname$.Time;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace $safeprojectname$;

public static class Extensions
{
    public static IServiceCollection AddInitializer<T>(this IServiceCollection services) where T : class, IInitializer
        => services.AddTransient<IInitializer, T>();
    public static T BindOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
        => BindOptions<T>(configuration.GetSection(sectionName));

    public static T BindOptions<T>(this IConfigurationSection section) where T : new()
    {
        var options = new T();
        section.Bind(options);
        return options;
    }
    public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
    {
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        return configuration.GetOptions<T>(sectionName);
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
    {
        var options = new T();
        configuration.GetSection(sectionName).Bind(options);
        return options;
    }
    public static IServiceCollection AddMicro(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("app");
        var options = section.BindOptions<AppOptions>();
        services.Configure<AppOptions>(section);

        return services
            .AddSingleton<IClock, UtcClock>()
            .AddSingleton<IIdGen>(new IdentityGenerator(options.GeneratorId))
            .AddSingleton<IJsonSerializer, SystemTextJsonSerializer>()
            .Configure<JsonOptions>(jsonOptions =>
            {
                jsonOptions.SerializerOptions.PropertyNameCaseInsensitive = true;
                jsonOptions.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                jsonOptions.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            });
    }
    public static string GetModuleName(this object value)
          => value?.GetType().GetModuleName() ?? string.Empty;

    public static string GetModuleName(this Type type)
    {
        if (type?.Namespace is null)
        {
            return string.Empty;
        }

        return type.Namespace.StartsWith("$ext_projectname$.Modules.")
            ? type.Namespace.Split(".")[2].ToLowerInvariant()
            : string.Empty;
    }
}