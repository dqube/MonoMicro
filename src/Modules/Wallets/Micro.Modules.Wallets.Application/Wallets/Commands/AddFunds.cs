using Micro.Abstractions.Abstractions;
using Micro.Modules.Wallets.Domain.Wallets.ValueObjects;

namespace Micro.Modules.Wallets.Application.Wallets.Commands;

internal record AddFunds(WalletId WalletId, string Currency, decimal Amount, string? TransferName = null,
    string? TransferMetadata = null) : ICommand
{
    public Guid TransferId { get; init; } = Guid.NewGuid();
}