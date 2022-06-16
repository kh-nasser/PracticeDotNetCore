using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities
{
    public static class UserUtils
    {
        public static long GetUserId(this ClaimsPrincipal? claim) {
            if (claim == null) return 0;

            var userId = claim.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Convert.ToInt64(userId);
        }
    }
}
