using Micro.DAL.SqlServer;
using Micro.Modules.Customers.Infrastructure.DAL;
using Micro.Testing;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Micro.Modules.Customers.Tests.Unit;

[ExcludeFromCodeCoverage]
internal sealed class TestDatabase : IDisposable
{
    public CustomersDbContext Context { get; }

    public TestDatabase()
    {
        var options = new OptionsProvider().Get<SqlServerOptions>("postgres");
        Context = new CustomersDbContext(new DbContextOptionsBuilder<CustomersDbContext>().UseSqlServer(options.ConnectionString).Options);
    }
    public Task InitAsync() => Context.Database.MigrateAsync();
    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}
