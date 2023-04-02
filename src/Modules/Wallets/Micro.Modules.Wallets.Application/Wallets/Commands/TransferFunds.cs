using Micro.Abstractions.Abstractions;

namespace Micro.Modules.Wallets.Application.Wallets.Commands;

internal record TransferFunds(Guid OwnerId, Guid OwnerWalletId, Guid ReceiverWalletId, string Currency,
    decimal Amount) : ICommand;