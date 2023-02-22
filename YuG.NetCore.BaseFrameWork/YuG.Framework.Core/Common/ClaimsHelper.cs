using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Core
{
    /// <summary>
    /// Claims帮助类
    /// </summary>
    public static class ClaimsHelper
    {
        /// <summary>
        /// 通过key获取claim值
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetClaimValue(this IEnumerable<Claim> claims, string key)
        {
            return claims.Select(e => e as Claim).FirstOrDefault(e => e.Type == key)?.Value;
        }
    }
}
