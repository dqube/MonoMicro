using Micro.Abstractions.Kernel.Types;
using Micro.Modules.Wallets.Domain.Owners.ValueObjects;
using Micro.Modules.Wallets.Domain.Wallets.Exceptions;
using Micro.Modules.Wallets.Domain.Wallets.ValueObjects;
using Microsoft.Extensions.Hosting;

namespace Micro.Modules.Wallets.Domain.Wallets.Entities;

internal class Wallet : Aggregate<WalletId>
{
    private HashSet<Transfer> _transfers = new();
    private Guid guid;
    private Guid customerId;
    private string currency;
    private DateTime dateTime;

    public OwnerId OwnerId { get; private set; }
    public Currency Currency { get; private set; }

    public IEnumerable<Transfer> Transfers
    {
        get => _transfers;
        set => _transfers = new HashSet<Transfer>(value);
    }

    public DateTime CreatedAt { get; private set; }



#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Wallet(WalletId id, OwnerId ownerId, Currency currency, DateTime createdAt) : base(id)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        Id = new WalletId(Guid.NewGuid());
        OwnerId = ownerId;
        Currency = currency;
        CreatedAt = createdAt;
    }

  

    public IReadOnlyCollection<Transfer> TransferFunds(Wallet receiver, Amount amount, DateTime createdAt)
    {
        var outTransferId = new TransferId();
        var inTransferId = new TransferId();

        var outTransfer = DeductFunds(outTransferId, amount, createdAt,name:string.Empty,
            metadata: GetMetadata(outTransferId, receiver.Id));

        var inTransfer = receiver.AddFunds(inTransferId, amount, createdAt, name:string.Empty,
            metadata: GetMetadata(inTransferId, Id));

        return new List<Transfer> { outTransfer, inTransfer };

        static TransferMetadata GetMetadata(TransferId referenceId, WalletId walletId)
            => new($"{{\"referenceId\": \"{referenceId}\", \"walletId\": \"{walletId}\"}}");
    }

    public IncomingTransfer AddFunds(TransferId transferId, Amount amount, DateTime createdAt,
        TransferName name, TransferMetadata metadata)
    {
        if (amount.Value <= 0)
        {
            throw new InvalidTransferAmountException(amount.Value);
        }

        if (string.IsNullOrWhiteSpace(name.Value))
        {
            name.Value = "add_funds";
        }

        var transfer = new IncomingTransfer(transferId, Id, Currency, amount, createdAt, name, metadata);
        _transfers.Add(transfer);
        IncrementVersion();

        return transfer;
    }

    public OutgoingTransfer DeductFunds(TransferId transferId, Amount amount, DateTime createdAt,
        TransferName name, TransferMetadata metadata)
    {
        if (amount.Value <= 0)
        {
            throw new InvalidTransferAmountException(amount.Value);
        }

        if (CurrentAmount().Value < amount.Value)
        {
            throw new InsufficientWalletFundsException(Id);
        }

        if (string.IsNullOrWhiteSpace(name.Value))
        {
            name = new TransferName("deduct_funds");
        }

        var transfer = new OutgoingTransfer(transferId, Id, Currency, amount, createdAt, name, metadata);
        _transfers.Add(transfer);
        IncrementVersion();

        return transfer;
    }

    public Amount CurrentAmount()
        => new Amount(_transfers.OfType<IncomingTransfer>().Sum(x => x.Amount.Value) - _transfers.OfType<OutgoingTransfer>().Sum(x => x.Amount.Value));
}