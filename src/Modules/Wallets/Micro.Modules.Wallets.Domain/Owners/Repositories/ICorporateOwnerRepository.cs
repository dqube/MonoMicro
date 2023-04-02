using Micro.Modules.Wallets.Domain.Owners.Entities;
using Micro.Modules.Wallets.Domain.Owners.ValueObjects;

namespace Micro.Modules.Wallets.Domain.Owners.Repositories;

internal interface ICorporateOwnerRepository
{
    Task<CorporateOwner> GetAsync(OwnerId id);
    Task AddAsync(CorporateOwner owner);
    Task UpdateAsync(CorporateOwner owner);
}