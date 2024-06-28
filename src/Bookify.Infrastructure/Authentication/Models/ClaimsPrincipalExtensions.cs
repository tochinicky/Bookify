using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Authentication.Models
{
    internal static class ClaimsPrincipalExtensions
    {
        public static string GetIdentityId(this ClaimsPrincipal? claimsPrincipal)
        {
            return claimsPrincipal?.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new ApplicationException("User identity is unavailable");
        }

        public static Guid GetUserId(this ClaimsPrincipal? claimsPrincipal)
        {
            var userId = claimsPrincipal?.FindFirstValue(JwtRegisteredClaimNames.Sub);
            
            return Guid.TryParse(userId, out Guid parsedUserId)? parsedUserId : throw new ApplicationException("User identifier is unavailable");
        }
    }
}
