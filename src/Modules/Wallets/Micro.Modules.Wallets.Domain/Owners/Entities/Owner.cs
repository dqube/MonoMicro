using Micro.Modules.Wallets.Domain.Owners.ValueObjects;

namespace Micro.Modules.Wallets.Domain.Owners.Entities;

internal abstract class Owner
{
    public OwnerId Id { get; private set; }
    public OwnerName Name { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? VerifiedAt { get; private set; }

    protected Owner()
    {
    }

    public Owner(OwnerId id, OwnerName name, DateTime createdAt)
    {
        Id = id;
        Name = name;
        IsActive = true;
        CreatedAt = createdAt;
    }

    public void Verify(DateTime verifiedAt)
    {
        VerifiedAt = verifiedAt;
    }

    public bool Lock() => IsActive = false;

    public bool Unlock() => IsActive = true;
}