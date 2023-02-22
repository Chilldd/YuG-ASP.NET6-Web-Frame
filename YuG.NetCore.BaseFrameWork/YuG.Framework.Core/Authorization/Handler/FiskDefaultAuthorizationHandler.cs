using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Authorization
{
    /// <summary>
    /// 默认授权处理程序(无验证操作)
    /// </summary>
    public class YuGDefaultAuthorizationHandler : AuthorizationHandler<IAuthorizationRequirement>
    {
        private readonly IAuthenticationSchemeProvider schemes;
        private readonly ILogger<YuGDefaultAuthorizationHandler> log;
        private readonly HttpContext httpContext;

        public YuGDefaultAuthorizationHandler(IAuthenticationSchemeProvider schemes,
                                             ILogger<YuGDefaultAuthorizationHandler> log,
                                             IHttpContextAccessor httpContext)
        {
            this.schemes = schemes;
            this.log = log;
            this.httpContext = httpContext.HttpContext;
        }

        /// <summary>
        /// 授权处理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IAuthorizationRequirement requirement)
        {
            try
            {
                //是否经过认证
                var isAuthenticated = context.User.Identity.IsAuthenticated;
                if (isAuthenticated)
                {
                    context.Succeed(requirement);
                }
                else
                    context.Fail();
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"【授权处理报错】");
                context.Fail();
            }

            await Task.CompletedTask;
        }
    }
}
