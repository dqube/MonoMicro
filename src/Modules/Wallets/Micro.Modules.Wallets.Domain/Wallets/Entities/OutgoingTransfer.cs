using System;
using Micro.Modules.Wallets.Domain.Owners.ValueObjects;
using Micro.Modules.Wallets.Domain.Wallets.ValueObjects;

namespace Micro.Modules.Wallets.Domain.Wallets.Entities;

internal class OutgoingTransfer : Transfer
{
    protected OutgoingTransfer()
    {
    }

    public OutgoingTransfer(TransferId id, WalletId walletId, Currency currency, Amount amount, DateTime createdAt,
        TransferName name, TransferMetadata metadata) : base(id, walletId, currency, amount,
        createdAt, name, metadata)
    {
    }
}