using Micro.Abstractions.Handlers;
using Micro.Modules.Wallets.Application.Wallets.DTO;
using Micro.Modules.Wallets.Application.Wallets.Queries;
using Micro.Modules.Wallets.Domain.Wallets.Entities;
using Micro.Modules.Wallets.Domain.Wallets.ValueObjects;
using Micro.Modules.Wallets.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Micro.Modules.Wallets.Infrastructure.Queries;

internal sealed class GetTransferHandler : IQueryHandler<GetTransfer, TransferDetailsDto>
{
    private readonly WalletsDbContext _context;
    private readonly DbSet<Wallet> _wallets;

    public GetTransferHandler(WalletsDbContext context)
    {
        _context = context;
        _wallets = _context.Wallets;
    }

    public async Task<TransferDetailsDto> HandleAsync(GetTransfer query, CancellationToken cancellationToken = default)
    {
        //var transfer = await _storage.FindAsync(x => x.Id == query.TransferId);
        var trasfer = _wallets
            .Include(x => x.Transfers)
            .SingleOrDefaultAsync(x => x.Id == query.TransferId);


        //return transfer?.AsDetailsDto();
        return new TransferDetailsDto();
    }
}