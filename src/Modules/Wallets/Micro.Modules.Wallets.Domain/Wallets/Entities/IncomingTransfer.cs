using Micro.Modules.Wallets.Domain.Wallets.ValueObjects;

namespace Micro.Modules.Wallets.Domain.Wallets.Entities;

internal class IncomingTransfer : Transfer
{
    protected IncomingTransfer()
    {
    }

    public IncomingTransfer(TransferId id, WalletId walletId, Currency currency, Amount amount, DateTime createdAt,
        TransferName name, TransferMetadata metadata) : base(id, walletId, currency, amount,
        createdAt, name, metadata)
    {
    }
}