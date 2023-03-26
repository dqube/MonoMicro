using Microsoft.IdentityModel.Tokens;

namespace $safeprojectname$.JWT;

internal sealed record SecurityKeyDetails(SecurityKey Key, string Algorithm);
