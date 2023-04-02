using Micro.Abstractions.Abstractions;

namespace Micro.Modules.Wallets.Application.Wallets.Commands;

internal record DeductFunds(Guid WalletId, string Currency, decimal Amount, string? TransferName = null,
    string? TransferMetadata = null) : ICommand
{
    public Guid TransferId { get; init; } = Guid.NewGuid();
}