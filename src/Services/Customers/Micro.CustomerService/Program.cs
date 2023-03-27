using Micro.Framework;

var builder = WebApplication
    .CreateBuilder(args)
    .AddModularFramework();

//builder.Services
//    .AddSingleton<ICustomersApiClient, CustomersApiClient>()
//    .AddPostgres<OrdersDbContext>(builder.Configuration)
//    .AddInbox<OrdersDbContext>(builder.Configuration)
//    .AddOutbox<OrdersDbContext>(builder.Configuration)
//    .AddTransactionalDecorators()
//    .AddOutboxInstantSenderDecorators()
//    .AddMessagingErrorHandlingDecorators();
var app = builder.Build();
app.UseModularFramework().Run();
