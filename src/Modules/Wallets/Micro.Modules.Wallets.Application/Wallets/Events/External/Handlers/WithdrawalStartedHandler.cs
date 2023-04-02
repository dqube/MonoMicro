using Micro.Abstractions.Handlers;
using Micro.Abstractions.Time;
using Micro.Messaging.Brokers;
using Micro.Modules.Wallets.Domain.Wallets.Exceptions;
using Micro.Modules.Wallets.Domain.Wallets.Repositories;
using Micro.Modules.Wallets.Domain.Wallets.ValueObjects;
using Microsoft.Extensions.Logging;


namespace Micro.Modules.Wallets.Application.Wallets.Events.External.Handlers;

internal class WithdrawalStartedHandler : IEventHandler<WithdrawalStarted>
{
    private const string TransferName = "withdrawal";
    private readonly IWalletRepository _walletRepository;
    private readonly IClock _clock;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<WithdrawalStartedHandler> _logger;

    public WithdrawalStartedHandler(IWalletRepository walletRepository, IClock clock,
        IMessageBroker messageBroker, ILogger<WithdrawalStartedHandler> logger)
    {
        _walletRepository = walletRepository;
        _clock = clock;
        _messageBroker = messageBroker;
        _logger = logger;
    }

    public async Task HandleAsync(WithdrawalStarted @event, CancellationToken cancellationToken = default)
    {
        var wallet = await _walletRepository.GetAsync(@event.CustomerId, @event.Currency);
        if (wallet is null)
        {
            throw new WalletNotFoundException(@event.CustomerId, @event.Currency);
        }

        try
        {
            var transfer = wallet.DeductFunds(Guid.NewGuid(), new Amount(@event.Amount), _clock.Current(),
                TransferName, GetMetadata(@event.WithdrawalId));
            await _messageBroker.SendAsync(new FundsDeducted(wallet.Id, wallet.OwnerId, wallet.Currency,
                @event.Amount, transfer.Name, transfer.Metadata), cancellationToken);
            _logger.LogInformation($"Deducted {@event.Amount} {wallet.Currency} from wallet with ID: '{wallet.Id}'" +
                                   $"based on started withdrawal with ID: '{@event.WithdrawalId}'.");
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            _logger.LogError($"Couldn't deduct {@event.Amount} {wallet.Currency} from wallet with ID: '{wallet.Id}'" +
                             $"based on started withdrawal with ID: '{@event.WithdrawalId}'.");
            //await _messageBroker.PublishAsync(new DeductFundsRejected(wallet.Id, wallet.OwnerId, wallet.Currency,
            //    @event.Amount, TransferName, GetMetadata(@event.WithdrawalId)), cancellationToken);
        }
        finally
        {
            await _walletRepository.UpdateAsync(wallet);
        }
    }

    private static string GetMetadata(Guid withdrawalId) => $"{{\"withdrawalId\": \"{withdrawalId}\"}}";
}