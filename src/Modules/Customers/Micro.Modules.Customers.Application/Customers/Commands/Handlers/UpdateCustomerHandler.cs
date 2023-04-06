using Micro.Abstractions.Handlers;
using Micro.Modules.Customers.Core.Customers.Entities;
using Micro.Modules.Customers.Core.Customers.Repositories;
using Microsoft.Extensions.Logging;

namespace Micro.Modules.Customers.Application.Customers.Handlers;

internal sealed class UpdateCustomerHandler : ICommandHandler<UpdateCustomer>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<AddCustomerHandler> _logger;
    public UpdateCustomerHandler(ICustomerRepository customerRepository, ILogger<AddCustomerHandler> logger)
    {
        _customerRepository = customerRepository;
        _logger = logger;
    }
    public async Task HandleAsync(UpdateCustomer command, CancellationToken cancellationToken = default)
    {
        var customer = Customer.Create(
           command.customerId,
           command.Name
           );
        await _customerRepository.AddAsync(customer);
        _logger.LogInformation($"Customer {command.customerId} updated sucessfully'.");
    }
}
