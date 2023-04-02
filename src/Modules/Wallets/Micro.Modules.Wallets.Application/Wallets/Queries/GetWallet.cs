using Micro.Abstractions.Abstractions;
using Micro.Modules.Wallets.Application.Wallets.DTO;

namespace Micro.Modules.Wallets.Application.Wallets.Queries;

internal class GetWallet : IQuery<WalletDetailsDto>
{
    public Guid WalletId { get; set; }
}