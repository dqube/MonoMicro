using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Customers.Core.Customers.Exceptions;

internal class InvalidTransferNameException : CustomException
{
    public string Name { get; }

    public InvalidTransferNameException(string name) : base($"Transfer name: '{name}' is invalid.")
    {
        Name = name;
    }
}