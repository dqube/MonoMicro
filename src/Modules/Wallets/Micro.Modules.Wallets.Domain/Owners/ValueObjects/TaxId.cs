using Micro.Abstractions.Kernel.Types;
using Micro.Modules.Wallets.Domain.Owners.Exceptions;

namespace Micro.Modules.Wallets.Domain.Owners.ValueObjects;

internal class TaxId : ValueObject
{
    public string Value { get; }

    public TaxId(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 20)
        {
            throw new InvalidTaxIdException(value);
        }

        Value = value.Trim();
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}