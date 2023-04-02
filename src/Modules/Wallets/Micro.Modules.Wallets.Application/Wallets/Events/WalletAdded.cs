using Micro.Abstractions.Abstractions;

namespace Micro.Modules.Wallets.Application.Wallets.Events;

internal record WalletAdded(Guid WalletId, Guid OwnerId, string Currency) : IEvent;