using Micro.Abstractions.Abstractions;

namespace Micro.Modules.Wallets.Application.Wallets.Events.External;

internal record WithdrawalStarted(Guid WithdrawalId, Guid CustomerId, string Currency, decimal Amount) : IEvent;