using YuG.Framework.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace YuG.Framework.Authorization
{
    /// <summary>
    /// 自定义返回结果
    /// </summary>
    public class YuGDefaultAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public YuGDefaultAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, 
                                  ILoggerFactory logger, 
                                  UrlEncoder encoder, 
                                  ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 401时触发(认证失败)
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.ContentType = "application/json";
            Response.StatusCode = StatusCodes.Status401Unauthorized;
            await Response.WriteAsync(JsonConvert.SerializeObject(BuildResultObject.BuildCustomizeResult(ResultEnum.NotAuth)));
        }

        /// <summary>
        /// 403时触发(鉴权失败)
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            Response.ContentType = "application/json";
            Response.StatusCode = StatusCodes.Status403Forbidden;
            await Response.WriteAsync(JsonConvert.SerializeObject(BuildResultObject.BuildCustomizeResult(ResultEnum.NotAPIAuth)));
        }
    }
}
