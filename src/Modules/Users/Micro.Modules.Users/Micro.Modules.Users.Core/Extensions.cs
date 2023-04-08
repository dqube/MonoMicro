using Microsoft.Extensions.DependencyInjection;


namespace Micro.Modules.Users.Core
{
    public static class Extensions
    {

        public static IServiceCollection AddDomain(this IServiceCollection services)
        => services;
    }
}