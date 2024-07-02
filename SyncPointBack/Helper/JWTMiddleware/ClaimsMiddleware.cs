using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SyncPointBack.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SyncPointBack.Helper.JWTMiddleware
{
    public static class ClaimsMiddleware
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }
    }
}