using Micro.Abstractions.Kernel.Types;
using Micro.Modules.Wallets.Domain.Owners.Exceptions;

namespace Micro.Modules.Wallets.Domain.Owners.ValueObjects;

public class FullName : ValueObject
{
    public string Value { get; }

    public FullName(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is > 100 or < 2)
        {
            throw new InvalidFullNameException(value);
        }

        Value = value;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}