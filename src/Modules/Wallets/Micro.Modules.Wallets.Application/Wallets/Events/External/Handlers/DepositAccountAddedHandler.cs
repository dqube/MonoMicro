using Micro.Abstractions.Handlers;
using Micro.Abstractions.Time;
using Micro.Messaging.Brokers;
using Micro.Modules.Wallets.Domain.Owners.ValueObjects;
using Micro.Modules.Wallets.Domain.Wallets.Entities;
using Micro.Modules.Wallets.Domain.Wallets.Repositories;
using Micro.Modules.Wallets.Domain.Wallets.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Micro.Modules.Wallets.Application.Wallets.Events.External.Handlers;

internal sealed class DepositAccountAddedHandler : IEventHandler<DepositAccountAdded>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IClock _clock;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<DepositAccountAddedHandler> _logger;

    public DepositAccountAddedHandler(IWalletRepository walletRepository, IClock clock,
        IMessageBroker messageBroker, ILogger<DepositAccountAddedHandler> logger)
    {
        _walletRepository = walletRepository;
        _clock = clock;
        _messageBroker = messageBroker;
        _logger = logger;
    }

    public async Task HandleAsync(DepositAccountAdded @event, CancellationToken cancellationToken = default)
    {
        var wallet = new Wallet(new WalletId(Guid.NewGuid()), new OwnerId(@event.CustomerId), @event.Currency, _clock.Current());
        await _walletRepository.AddAsync(wallet);
        await _messageBroker.SendAsync(new WalletAdded(wallet.Id, wallet.OwnerId, wallet.Currency),
            cancellationToken);
        _logger.LogInformation($"Created a new wallet with ID: '{wallet.Id}', owner ID: '{wallet.OwnerId}', " +
                               $"currency: '{@event.Currency}' for deposit account with ID: '{@event.AccountId}'.");
    }
}