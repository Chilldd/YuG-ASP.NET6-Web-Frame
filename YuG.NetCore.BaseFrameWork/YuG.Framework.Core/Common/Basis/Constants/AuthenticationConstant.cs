using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Core
{
    public static class AuthenticationConstant
    {
        /// <summary>
        /// 授权机制
        /// </summary>
        public static string SecurityDefinition = "oauth2";

        /// <summary>
        /// 身份认证：Http请求头中Token对应的key
        /// </summary>
        public static string AuthorizationHeaderName = "Authorization";

        /// <summary>
        /// Http认证方案
        /// </summary>

        public static string AuthenticationSchemeStr = $"{JwtBearerDefaults.AuthenticationScheme} "; 

        /// <summary>
        /// Token名称
        /// </summary>
        public static string TokenName = "access_token";
    }
}
