using System.Security.Cryptography.X509Certificates;

namespace $safeprojectname$.Vault;

public interface ICertificatesIssuer
{
    Task<X509Certificate2?> IssueAsync();
}