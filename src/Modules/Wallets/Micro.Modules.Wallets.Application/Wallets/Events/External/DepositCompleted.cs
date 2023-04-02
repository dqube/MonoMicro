using Micro.Abstractions.Abstractions;

namespace Micro.Modules.Wallets.Application.Wallets.Events.External;

internal record DepositCompleted(Guid DepositId, Guid CustomerId, string Currency, decimal Amount) : IEvent;