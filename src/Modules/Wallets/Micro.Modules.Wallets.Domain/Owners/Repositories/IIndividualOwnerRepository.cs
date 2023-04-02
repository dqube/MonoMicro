using Micro.Modules.Wallets.Domain.Owners.Entities;
using Micro.Modules.Wallets.Domain.Owners.ValueObjects;

namespace Micro.Modules.Wallets.Domain.Owners.Repositories;

internal interface IIndividualOwnerRepository
{
    Task<IndividualOwner> GetAsync(OwnerId id);
    Task AddAsync(IndividualOwner owner);
    Task UpdateAsync(IndividualOwner owner);
}