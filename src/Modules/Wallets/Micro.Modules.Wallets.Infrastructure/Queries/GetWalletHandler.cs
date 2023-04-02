using IdGen;
using Micro.Abstractions.Handlers;
using Micro.Modules.Wallets.Application.Wallets.DTO;
using Micro.Modules.Wallets.Application.Wallets.Queries;
using Micro.Modules.Wallets.Domain.Wallets.Entities;
using Micro.Modules.Wallets.Domain.Wallets.ValueObjects;
using Micro.Modules.Wallets.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Micro.Modules.Wallets.Infrastructure.Queries;

internal sealed class GetWalletHandler : IQueryHandler<GetWallet, WalletDetailsDto>
{
    private readonly WalletsDbContext _context;
    private readonly DbSet<Wallet> _wallets;

    public GetWalletHandler(WalletsDbContext context)
    {
        _context = context;
        _wallets = _context.Wallets;
    }

  

    public async Task<WalletDetailsDto> HandleAsync(GetWallet query, CancellationToken cancellationToken = default)
    {
        // Owner cannot access the other wallets
        var wallet= _wallets
            .Include(x => x.Transfers)
            .SingleOrDefaultAsync(x => x.Id == new WalletId(query.WalletId));
       
        return new WalletDetailsDto();
       // return wallet.AsDetailsDto();
    }
}