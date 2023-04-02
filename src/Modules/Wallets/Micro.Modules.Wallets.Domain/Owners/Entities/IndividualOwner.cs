using Micro.Modules.Wallets.Domain.Owners.ValueObjects;

namespace Micro.Modules.Wallets.Domain.Owners.Entities;

internal class IndividualOwner : Owner
{
    public FullName FullName { get; private set; }

    private IndividualOwner()
    {
    }

    public IndividualOwner(OwnerId id, OwnerName name, FullName fullName, DateTime createdAt) : base(id, name, createdAt)
    {
        FullName = fullName;
    }
}