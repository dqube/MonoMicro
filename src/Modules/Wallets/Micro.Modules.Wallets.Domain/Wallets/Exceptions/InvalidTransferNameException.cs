using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Wallets.Domain.Wallets.Exceptions;

internal class InvalidTransferNameException : CustomException
{
    public string Name { get; }

    public InvalidTransferNameException(string name) : base($"Transfer name: '{name}' is invalid.")
    {
        Name = name;
    }
}