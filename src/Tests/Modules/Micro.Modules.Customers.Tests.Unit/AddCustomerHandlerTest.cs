using Micro.Abstractions.Handlers;
using Micro.Modules.Customers.Application.Clients.Users;
using Micro.Modules.Customers.Application.Customers;
using Micro.Modules.Customers.Application.Customers.Handlers;
using Micro.Modules.Customers.Core.Customers.Repositories;
using Micro.Modules.Customers.Infrastructure.DAL.Repositories;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace Micro.Modules.Customers.Tests.Unit;

[ExcludeFromCodeCoverage]
public class AddCustomerHandlerTests : IDisposable
{
    private Task Act(AddCustomer command) => _handler.HandleAsync(command);

    public async Task given_valid_command_adding_resource_should_succeed_and_publish_an_event()
    {
        await _testDatabase.InitAsync();

        var command = new AddCustomer(new Core.Customers.ValueObjects.CustomerId(1),"test");

        await Act(command);

        var resource = await _resourcesRepository.GetAsync(command.customerId);
        //resource.();
     
        //resourceAdded.ShouldNotBeNull();
    }

    #region Arrange

    private readonly TestDatabase _testDatabase;
    private readonly ICustomerRepository _resourcesRepository;
    private readonly ICommandHandler<AddCustomer> _handler;
    private readonly ILogger<AddCustomerHandler> _logger;
    private readonly IUserApiClient _userApiClient;
    public AddCustomerHandlerTests()
    {
        _testDatabase = new TestDatabase();
        _resourcesRepository = new CustomerRepository(_testDatabase.Context);

        _handler = new AddCustomerHandler(_resourcesRepository,_logger,_userApiClient);
    }

    #endregion

    public void Dispose()
    {
        _testDatabase.Dispose();
    }
}
