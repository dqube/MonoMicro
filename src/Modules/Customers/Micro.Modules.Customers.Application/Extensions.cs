using FluentValidation;
using Micro.Modules.Customers.Application.Customers;
using Micro.Modules.Customers.Application.Customers.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.Modules.Customers.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //services.AddScoped<IValidator<AddCustomer>, CustomerValidator>();
        return services;
    }
}
