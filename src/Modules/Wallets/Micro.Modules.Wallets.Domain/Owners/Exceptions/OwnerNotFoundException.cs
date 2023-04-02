using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Wallets.Domain.Owners.Exceptions;

public class OwnerNotFoundException : CustomException
{
    public Guid OwnerId { get; }

    public OwnerNotFoundException(Guid ownerId) : base($"Owner with ID: '{ownerId}' was not found.")
    {
        OwnerId = ownerId;
    }
}