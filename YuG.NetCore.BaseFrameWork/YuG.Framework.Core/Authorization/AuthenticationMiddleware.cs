using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.Extensions.Configuration;
using YuG.Framework.Core;

namespace YuG.Framework.Authorization
{
    /// <summary>
    /// 认证授权中间件
    /// </summary>
    public static class AuthenticationMiddleware
    {

        public static void UseAuthenticationMiddleware(this IApplicationBuilder app, IConfiguration configuration)
        {
            var option = configuration.GetSection(AuthenticationOption.AuthenticationOptionName).Get<AuthenticationOption>();
            if (option is null)
                throw new YuGCustomizeException($"【{SystemServerEnum.Authorization.GetEnumDescription()}】缺少配置信息【{AuthenticationOption.AuthenticationOptionName}】");

            app.UseAuthentication();
            app.UseAuthorization();
            SystemServerInformationHelper.OpenMiddlewareInformation(SystemServerEnum.Authorization);
        }
    }
}
