using Micro.Abstractions.Handlers;
using Micro.Abstractions.Time;
using Micro.Messaging.Brokers;
using Micro.Modules.Wallets.Application.Wallets.Events;
using Micro.Modules.Wallets.Domain.Wallets.Exceptions;
using Micro.Modules.Wallets.Domain.Wallets.Repositories;
using Micro.Modules.Wallets.Domain.Wallets.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Micro.Modules.Wallets.Application.Wallets.Commands.Handlers;

internal sealed class DeductFundsHandler : ICommandHandler<DeductFunds>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IClock _clock;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<DeductFundsHandler> _logger;

    public DeductFundsHandler(IWalletRepository walletRepository, IClock clock, IMessageBroker messageBroker,
        ILogger<DeductFundsHandler> logger)
    {
        _walletRepository = walletRepository;
        _clock = clock;
        _messageBroker = messageBroker;
        _logger = logger;
    }

    public async Task HandleAsync(DeductFunds command, CancellationToken cancellationToken = default)
    {
        var wallet = await _walletRepository.GetAsync(new WalletId(command.WalletId));
        if (wallet is null)
        {
            throw new WalletNotFoundException(command.WalletId);
        }

        if (wallet.Currency != command.Currency)
        {
            throw new InvalidTransferCurrencyException(command.Currency);
        }

        var transfer = wallet.DeductFunds(command.TransferId, new Amount(command.Amount), _clock.Current(),
            command.TransferName, command.TransferMetadata);
        await _walletRepository.UpdateAsync(wallet);
        await _messageBroker.SendAsync(new FundsDeducted(wallet.Id, wallet.OwnerId, wallet.Currency,
            transfer.Amount, transfer.Name, transfer.Metadata), cancellationToken);
        _logger.LogInformation($"Deducted {transfer.Amount} {transfer.Currency} from wallet with ID: '{wallet.Id}'.");
    }
}