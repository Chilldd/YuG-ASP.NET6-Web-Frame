using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.WebAPI.Extends
{
    /// <summary>
    /// 自定义授权处理程序
    /// </summary>
    public class CustomizeAuthorizationHandler : AuthorizationHandler<IAuthorizationRequirement>
    {
        private readonly IAuthenticationSchemeProvider schemes;
        private readonly ILogger<CustomizeAuthorizationHandler> log;
        private readonly HttpContext httpContext;

        /// <summary>
        /// init
        /// </summary>
        /// <param name="schemes"></param>
        /// <param name="log"></param>
        /// <param name="httpContext"></param>
        public CustomizeAuthorizationHandler(IAuthenticationSchemeProvider schemes,
                                             ILogger<CustomizeAuthorizationHandler> log,
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
                    Console.WriteLine("Customize");
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
