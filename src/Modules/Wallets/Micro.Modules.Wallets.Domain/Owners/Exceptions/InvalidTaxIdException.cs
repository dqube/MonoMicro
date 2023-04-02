using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Wallets.Domain.Owners.Exceptions;

internal class InvalidTaxIdException : CustomException
{
    public string TaxId { get; }

    public InvalidTaxIdException(string taxId) : base($"Tax ID: '{taxId}' is invalid.")
    {
        TaxId = taxId;
    }
}