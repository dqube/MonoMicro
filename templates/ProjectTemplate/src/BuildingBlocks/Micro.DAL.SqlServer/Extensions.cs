using Humanizer.Configuration;
using $ext_projectname$.Abstractions;
using $ext_projectname$.Abstractions.Handlers;
using $ext_projectname$.Abstractions.Kernel.Types;
using $safeprojectname$.Internals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace $safeprojectname$;

public static class Extensions
{
    public static IServiceCollection AddSqlServer<T>(this IServiceCollection services, IConfiguration configuration)
        where T : DbContext
    {
        var section = configuration.GetSection("sqlserver");
        var options = section.BindOptions<SqlServerOptions>();
        services.Configure<SqlServerOptions>(section);
        if (!section.Exists())
        {
            return services;
        }

        services.AddDbContext<T>(x => x.UseSqlServer(options.ConnectionString));
        services.AddHostedService<DatabaseInitializer<T>>();
        services.AddHostedService<DataInitializer>();
        services.AddScoped<IUnitOfWork, SqlServerUnitOfWork<T>>();
        
        // Temporary fix for EF Core issue related to https://github.com/npgsql/efcore.pg/issues/2000
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    }
    public static void FilterSoftDeletedProperties(this ModelBuilder modelBuilder)
    {
        Expression<Func<IAggregate, bool>> filterExpr = e => !e.IsDeleted;
        foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes()
                     .Where(m => m.ClrType.IsAssignableTo(typeof(IEntity))))
        {
            // modify expression to handle correct child type
            var parameter = Expression.Parameter(mutableEntityType.ClrType);
            var body = ReplacingExpressionVisitor
                .Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
            var lambdaExpression = Expression.Lambda(body, parameter);

            // set filter
            mutableEntityType.SetQueryFilter(lambdaExpression);
        }
    }
    public static IServiceCollection AddInitializer<T>(this IServiceCollection services) where T : class, IDataInitializer
        => services.AddTransient<IDataInitializer, T>();

    public static IServiceCollection AddSqlServerModule(this IServiceCollection services)
    {
        var options = services.GetOptions<SqlServerOptions>("sqlserver");
        services.AddSingleton(options);
        services.AddSingleton(new UnitOfWorkTypeRegistry());        
        return services;
    }

    //public static IServiceCollection AddTransactionalDecorators(this IServiceCollection services)
    //{
    //    services.TryDecorate(typeof(ICommandHandler<>), typeof(TransactionalCommandHandlerDecorator<>));
    //    services.TryDecorate(typeof(IEventHandler<>), typeof(TransactionalEventHandlerDecorator<>));

    //    return services;
    //}

    public static IServiceCollection AddSqlServerModule<T>(this IServiceCollection services) where T : DbContext
    {
        var options = services.GetOptions<SqlServerOptions>("sqlserver");
        services.AddDbContext<T>(x => x.UseSqlServer(options.ConnectionString));
        return services;
    }

    public static IServiceCollection AddUnitOfWork<T>(this IServiceCollection services) where T : class, IUnitOfWork
    {
        services.AddScoped<IUnitOfWork, T>();
        services.AddScoped<T>();
        using var serviceProvider = services.BuildServiceProvider();
        serviceProvider.GetRequiredService<UnitOfWorkTypeRegistry>().Register<T>();

        return services;
    }
}