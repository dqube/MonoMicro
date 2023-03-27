using Microsoft.Extensions.DependencyInjection;

namespace Micro.Modules.Customers.Persistence;

public static class Extensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    => services;
}
