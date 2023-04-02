using Micro.Abstractions.Abstractions;

namespace Micro.Modules.Wallets.Application.Wallets.Events;

internal record FundsDeducted(Guid WalletId, Guid OwnerId, string Currency, decimal Amount, string TransferName = null,
    string TransferMetadata = null) : IEvent;