using Micro.Abstractions.Pagination;
using Micro.Modules.Wallets.Application.Wallets.DTO;

namespace Micro.Modules.Wallets.Application.Wallets.Queries;

internal class BrowseWallets : PagedQuery<WalletDto>
{
    public Guid? OwnerId { get; set; }
    public string Currency { get; set; }
}