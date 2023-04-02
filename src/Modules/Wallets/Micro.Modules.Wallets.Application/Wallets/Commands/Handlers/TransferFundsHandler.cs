using Micro.Abstractions.Abstractions;
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

internal class TransferFundsHandler : ICommandHandler<TransferFunds>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IClock _clock;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<TransferFundsHandler> _logger;

    public TransferFundsHandler(IWalletRepository walletRepository, IClock clock, IMessageBroker messageBroker,
        ILogger<TransferFundsHandler> logger)
    {
        _walletRepository = walletRepository;
        _clock = clock;
        _messageBroker = messageBroker;
        _logger = logger;
    }

    public async Task HandleAsync(TransferFunds command, CancellationToken cancellationToken = default)
    {
        var amount = new Amount(command.Amount);
        var ownerWallet = await _walletRepository.GetAsync(command.OwnerWalletId);
        if (ownerWallet is null || ownerWallet.OwnerId.Value != command.OwnerId)
        {
            throw new WalletNotFoundException(command.OwnerWalletId);
        }

        if (ownerWallet.Currency.Value != command.Currency)
        {
            throw new InvalidTransferCurrencyException(command.Currency);
        }

        var receiverWallet = await _walletRepository.GetAsync(command.ReceiverWalletId);
        if (receiverWallet is null)
        {
            throw new WalletNotFoundException(command.ReceiverWalletId);
        }

        if (receiverWallet.Currency.Value != command.Currency)
        {
            throw new InvalidTransferCurrencyException(command.Currency);
        }

        var now = _clock.Current();
        var transfers = ownerWallet.TransferFunds(receiverWallet, amount, now);
        var outgoingTransfer = transfers.OfType<OutgoingTransfer>().Single();
        var incomingTransfer = transfers.OfType<IncomingTransfer>().Single();
        await _walletRepository.UpdateAsync(ownerWallet);
        await _walletRepository.UpdateAsync(receiverWallet);
        //await _messageBroker.SendAsync(new IMessage[]
        //{
        //    new FundsDeducted(ownerWallet.Id, ownerWallet.OwnerId, ownerWallet.Currency,
        //        outgoingTransfer.Amount, outgoingTransfer.Name, outgoingTransfer.Metadata),
        //    new FundsAdded(receiverWallet.Id, receiverWallet.OwnerId, receiverWallet.Currency,
        //        incomingTransfer.Amount, incomingTransfer.Name, incomingTransfer.Metadata)
        //}, cancellationToken);
        _logger.LogInformation($"Transferred {outgoingTransfer.Amount} {outgoingTransfer.Currency}" +
                               $"from wallet with ID: '{ownerWallet.Id}' to wallet with ID: '{receiverWallet.Id}'.");
    }
}