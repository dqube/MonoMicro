using Micro.Abstractions.Abstractions;

namespace Micro.Modules.Wallets.Application.Wallets.Events;

internal record FundsLocked(Guid WalletId, string Currency, decimal Amount) : IEvent;