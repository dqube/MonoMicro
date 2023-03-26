using $safeprojectname$.Encryption;
using $safeprojectname$.Hashing;
using $safeprojectname$.Random;
using $safeprojectname$.Signing;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace $safeprojectname$;

public static class Extensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("security");
        services.Configure<SecurityOptions>(section);

        services
            .AddSingleton<IEncryptor, AesEncryptor>()
            .AddSingleton<IShaHasher, ShaHasher>()
            .AddSingleton<IRng, Rng>()
            .AddSingleton<ISigner, Signer>()
            .AddSingleton<IPasswordManager, PasswordManager>()
            .AddSingleton<IPasswordHasher<object>, PasswordHasher<object>>();

        return services;
    }
}