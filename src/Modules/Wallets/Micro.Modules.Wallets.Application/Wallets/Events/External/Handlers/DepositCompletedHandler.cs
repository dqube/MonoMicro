using Micro.Abstractions.Handlers;
using Micro.Abstractions.Time;
using Micro.Messaging.Brokers;
using Micro.Modules.Wallets.Domain.Wallets.Exceptions;
using Micro.Modules.Wallets.Domain.Wallets.Repositories;
using Micro.Modules.Wallets.Domain.Wallets.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Micro.Modules.Wallets.Application.Wallets.Events.External.Handlers;

internal class DepositCompletedHandler : IEventHandler<DepositCompleted>
{
    private const string TransferName = "deposit";
    private readonly IWalletRepository _walletRepository;
    private readonly IClock _clock;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<DepositCompletedHandler> _logger;

    public DepositCompletedHandler(IWalletRepository walletRepository, IClock clock, IMessageBroker messageBroker,
        ILogger<DepositCompletedHandler> logger)
    {
        _walletRepository = walletRepository;
        _clock = clock;
        _messageBroker = messageBroker;
        _logger = logger;
    }

    public async Task HandleAsync(DepositCompleted @event, CancellationToken cancellationToken = default)
    {
        var wallet = await _walletRepository.GetAsync(@event.CustomerId, @event.Currency);
        if (wallet is null)
        {
            throw new WalletNotFoundException(@event.CustomerId, @event.Currency);
        }

        var transfer = wallet.AddFunds(Guid.NewGuid(), new Amount(@event.Amount), _clock.Current(),
            TransferName, GetMetadata(@event.DepositId));
        await _walletRepository.UpdateAsync(wallet);
        await _messageBroker.SendAsync(new FundsAdded(wallet.Id, wallet.OwnerId, wallet.Currency,
            @event.Amount, transfer.Name, transfer.Metadata), cancellationToken);
        _logger.LogInformation($"Added {@event.Amount} {wallet.Currency} to wallet with ID: '{wallet.Id}'" +
                               $"based on completed deposit with ID: '{@event.DepositId}'.");
    }

    private static string GetMetadata(Guid depositId) => $"{{\"depositId\": \"{depositId}\"}}";
}