using Micro.Modules.Wallets.Domain.Owners.ValueObjects;

namespace Micro.Modules.Wallets.Domain.Owners.Entities;

internal class CorporateOwner : Owner
{
    public TaxId TaxId { get; private set; }

    private CorporateOwner()
    {
    }

    public CorporateOwner(OwnerId id, OwnerName name, TaxId taxId, DateTime createdAt) : base(id, name, createdAt)
    {
        TaxId = taxId;
    }
}