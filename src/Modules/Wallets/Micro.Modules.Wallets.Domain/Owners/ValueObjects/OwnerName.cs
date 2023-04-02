using Micro.Abstractions.Kernel.Types;
using Micro.Modules.Wallets.Domain.Owners.Exceptions;

namespace Micro.Modules.Wallets.Domain.Owners.ValueObjects;

internal class OwnerName : ValueObject
{
    public string Value { get; }

    public OwnerName(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
        {
            throw new InvalidOwnerNameException(value);
        }

        Value = value.Trim().ToLowerInvariant().Replace(" ", ".");
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}