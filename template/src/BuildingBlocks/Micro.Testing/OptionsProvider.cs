using System.Diagnostics.CodeAnalysis;
using $ext_projectname$.Abstractions;
using Microsoft.Extensions.Configuration;

namespace $safeprojectname$;

[ExcludeFromCodeCoverage]
public sealed class OptionsProvider
{
    private readonly IConfigurationRoot _configuration;

    public OptionsProvider(string settingsPath = "appsettings.test.json")
    {
        _configuration = GetConfigurationRoot(settingsPath);
    }

    public T Get<T>(string sectionName) where T : class, new() => _configuration.BindOptions<T>(sectionName);
    
    private static IConfigurationRoot GetConfigurationRoot(string settingsPath)
        => new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true)
            .AddJsonFile(settingsPath, true)
            .AddEnvironmentVariables()
            .Build();
}