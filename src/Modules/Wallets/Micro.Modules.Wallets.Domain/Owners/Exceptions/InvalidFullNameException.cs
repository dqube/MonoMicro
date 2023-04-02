using Micro.Abstractions.Exceptions;

namespace Micro.Modules.Wallets.Domain.Owners.Exceptions;

public class InvalidFullNameException : CustomException
{
    public string FullName { get; }

    public InvalidFullNameException(string fullName) : base($"Full name: '{fullName}' is invalid.")
    {
        FullName = fullName;
    }
}