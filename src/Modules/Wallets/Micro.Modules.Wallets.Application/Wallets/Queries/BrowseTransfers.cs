using Micro.Abstractions.Pagination;
using Micro.Modules.Wallets.Application.Wallets.DTO;

namespace Micro.Modules.Wallets.Application.Wallets.Queries;

internal class BrowseTransfers : PagedQuery<TransferDto>
{
    public string Currency { get; set; }
    public string Name { get; set; }
}