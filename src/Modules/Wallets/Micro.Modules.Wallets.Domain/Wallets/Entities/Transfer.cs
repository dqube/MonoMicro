using Micro.Modules.Wallets.Domain.Wallets.ValueObjects;

namespace Micro.Modules.Wallets.Domain.Wallets.Entities;

internal abstract class Transfer
{
    public TransferId Id { get; private set; }
    public WalletId WalletId { get; private set; }
    public Currency Currency { get; private set; }
    public Amount Amount { get; private set; }
    public TransferName Name { get; private set; }
    public TransferMetadata Metadata { get; private set; }
    public DateTime CreatedAt { get; private set; }

    protected Transfer()
    {
    }

    protected Transfer(TransferId id, WalletId walletId, Currency currency, Amount amount, DateTime createdAt,
        TransferName name, TransferMetadata metadata)
    {
        Id = id;
        WalletId = walletId;
        Currency = currency;
        Amount = amount;
        CreatedAt = createdAt;
        Name = name;
        Metadata = metadata;
    }
}