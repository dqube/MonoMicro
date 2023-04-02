using Micro.Abstractions.Abstractions;

namespace Micro.Modules.Wallets.Application.Wallets.Events.External;

internal record DepositAccountAdded(Guid AccountId, Guid CustomerId, string Currency) : IEvent;