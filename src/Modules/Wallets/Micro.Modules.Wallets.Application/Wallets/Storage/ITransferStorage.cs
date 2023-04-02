using Micro.Abstractions.Pagination;
using Micro.Modules.Wallets.Domain.Wallets.Entities;
using System.Linq.Expressions;

namespace Micro.Modules.Wallets.Application.Wallets.Storage;

internal interface ITransferStorage
{
    Task<Transfer> FindAsync(Expression<Func<Transfer, bool>> expression);
   // Task<Paged<Transfer>> BrowseAsync(Expression<Func<Transfer, bool>> expression, IPagedQuery query);
}