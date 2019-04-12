using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using UniversitySchedule.Core.Enums;
using UniversitySchedule.Core.ExceptionTypes;

namespace UniversitySchedule.DI.Managers
{
    public static class TokenManager
    {
        public static long GetPartnerId(ClaimsPrincipal user)
        {
            var identity = (ClaimsIdentity)user.Identity;
            var claims = identity.Claims;
            var partnerId = -1;
            var claim = claims.FirstOrDefault(x => x.Type.Equals(nameof(partnerId)));

            if (claim == null)
            {
                throw new LogicException(ExceptionMessage.TOKEN_EXPIRED);
            }

            partnerId = Convert.ToInt32(claim.Value);

            return partnerId;
        }
        public static long GetClientId(ClaimsPrincipal user)
        {
            var identity = (ClaimsIdentity)user.Identity;

            return Convert.ToInt64(identity.Name);
        }
        public static int GetBackofficeAccountId()
        {
            throw new NotImplementedException();
        }
        public static string GetCasinoToken(ClaimsPrincipal user)
        {
            var identity = (ClaimsIdentity)user.Identity;
            var claims = identity.Claims;
            var casinoToken = "";
            var claim = claims.FirstOrDefault(x => x.Type.Equals(nameof(casinoToken)));
            casinoToken = claim.Value;

            return casinoToken;
        }
        public static string GetRole(ClaimsPrincipal user)
        {
            var identity = (ClaimsIdentity)user.Identity;
            var role = identity.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Role)?.Value;

            return role;
        }
    }
}
