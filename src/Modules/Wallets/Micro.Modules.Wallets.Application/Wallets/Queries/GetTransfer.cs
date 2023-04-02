using Micro.Abstractions.Abstractions;
using Micro.Modules.Wallets.Application.Wallets.DTO;

namespace Micro.Modules.Wallets.Application.Wallets.Queries;

internal class GetTransfer : IQuery<TransferDetailsDto>
{
    public Guid? TransferId { get; set; }
}