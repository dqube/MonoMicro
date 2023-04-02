using System;

namespace Micro.Modules.Wallets.Application.Wallets.DTO;

internal class WalletDto
{
    public Guid WalletId { get; set; }
    public Guid OwnerId { get; set; }
    public string Currency { get; set; }
    public DateTime CreatedAt { get; set; }
}