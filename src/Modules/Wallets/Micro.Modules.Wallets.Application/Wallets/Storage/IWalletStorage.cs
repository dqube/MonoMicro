using Micro.Modules.Wallets.Domain.Wallets.Entities;
using System.Linq.Expressions;

namespace Micro.Modules.Wallets.Application.Wallets.Storage;

internal interface IWalletStorage
{
    Task<Wallet> FindAsync(Expression<Func<Wallet, bool>> expression);
    //Task<Paged<Wallet>> BrowseAsync(Expression<Func<Wallet, bool>> expression, IPagedQuery query);
}