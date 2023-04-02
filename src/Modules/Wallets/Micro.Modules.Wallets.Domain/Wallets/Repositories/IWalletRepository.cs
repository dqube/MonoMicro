using Micro.Modules.Wallets.Domain.Owners.ValueObjects;
using Micro.Modules.Wallets.Domain.Wallets.Entities;
using Micro.Modules.Wallets.Domain.Wallets.ValueObjects;

namespace Micro.Modules.Wallets.Domain.Wallets.Repositories;

internal interface IWalletRepository
{
    Task<Wallet> GetAsync(WalletId id);
    Task<Wallet> GetAsync(OwnerId ownerId, Currency currency);
    Task AddAsync(Wallet wallet);
    Task UpdateAsync(Wallet wallet);
}
