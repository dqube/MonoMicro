using Micro.Abstractions.Handlers;
using Micro.Abstractions.Time;
using Micro.Messaging.Brokers;
using Micro.Modules.Wallets.Application.Wallets.Events;
using Micro.Modules.Wallets.Domain.Wallets.Entities;
using Micro.Modules.Wallets.Domain.Wallets.Exceptions;
using Micro.Modules.Wallets.Domain.Wallets.Repositories;
using Micro.Modules.Wallets.Domain.Wallets.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Micro.Modules.Wallets.Application.Wallets.Commands.Handlers;

internal sealed class AddFundsHandler : ICommandHandler<AddFunds>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IClock _clock;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<AddFundsHandler> _logger;

    public AddFundsHandler(IWalletRepository walletRepository, IClock clock, IMessageBroker messageBroker,
        ILogger<AddFundsHandler> logger)
    {
        _walletRepository = walletRepository;
        _clock = clock;
        _messageBroker = messageBroker;
        _logger = logger;
    }

    public async Task HandleAsync(AddFunds command, CancellationToken cancellationToken = default)
    {
        Wallet wallet = await _walletRepository.GetAsync(command.WalletId);
        if (wallet is null)
        {
            throw new WalletNotFoundException(command.WalletId);
        }

        if (wallet.Currency != command.Currency)
        {
            throw new InvalidTransferCurrencyException(command.Currency);
        }

        var transfer = wallet.AddFunds(command.TransferId, new Amount(command.Amount), _clock.Current(),
            command.TransferName, command.TransferMetadata);
        await _walletRepository.UpdateAsync(wallet);
        await _messageBroker.SendAsync(new FundsAdded(wallet.Id, wallet.OwnerId, wallet.Currency,
            transfer.Amount, transfer.Name, transfer.Metadata), cancellationToken);
        _logger.LogInformation($"Added {transfer.Amount} {transfer.Currency} to wallet with ID: '{wallet.Id}'.");
    }
}