using BASEREF-NAME.Abstractions.Handlers;
using Micro.Modules.Customers.Core.Customers.Entities;
using Micro.Modules.Customers.Core.Customers.Repositories;
using Microsoft.Extensions.Logging;

namespace Micro.Modules.Customers.Application.Customers.Handlers;

internal sealed class AddCustomerHandler : ICommandHandler<AddCustomer>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<AddCustomerHandler> _logger;
    public AddCustomerHandler(ICustomerRepository customerRepository, ILogger<AddCustomerHandler> logger)
    {
        _customerRepository = customerRepository;
        _logger = logger;
    }



    public async Task HandleAsync(AddCustomer command, CancellationToken cancellationToken = default)
    {
        var customer = Customer.Create(
           command.customerId,
           command.Name
           );
        await _customerRepository.AddAsync(customer);
        _logger.LogInformation($"Customer {command.customerId} added sucessfully'.");
        //throw new NotImplementedException();
    }
}
