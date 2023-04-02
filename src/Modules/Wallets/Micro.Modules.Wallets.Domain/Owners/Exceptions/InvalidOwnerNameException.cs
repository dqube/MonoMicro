using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Wallets.Domain.Owners.Exceptions;

internal class InvalidOwnerNameException : CustomException
{
    public string Name { get; }

    public InvalidOwnerNameException(string name) : base($"Owner name: '{name}' is invalid.")
    {
        Name = name;
    }
}