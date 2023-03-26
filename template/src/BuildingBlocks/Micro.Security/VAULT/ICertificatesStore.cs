using System.Security.Cryptography.X509Certificates;

namespace $safeprojectname$.Vault;

public interface ICertificatesStore
{
    IReadOnlyDictionary<string, X509Certificate2> All { get; }
    X509Certificate2? Get(string name);
    void Set(string name, X509Certificate2 certificate);
}