using YuG.Framework.Authorization;
using YuG.Framework.Core;
using YuG.Framework.Core.Cache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YuG.Framework.WebAPI.Controllers
{
    /// <summary>
    /// 测试API
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class APITestController : ControllerBase
    {
        private readonly IUserInfoHelper userInfoHelper;
        private readonly ITokenHelper tokenHelper;
        private readonly ILogger<APITestController> logger;

        /// <summary>
        /// Test
        /// </summary>
        /// <param name="userInfoHelper"></param>
        /// <param name="tokenHelper"></param>
        public APITestController(IUserInfoHelper userInfoHelper,
                                 ITokenHelper tokenHelper,
                                 ILogger<APITestController> logger)
        {
            this.userInfoHelper = userInfoHelper;
            this.tokenHelper = tokenHelper;
            this.logger = logger;
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public string GetToken(int id = 1, int? tenant = null)
        {
            return "Bearer " + tokenHelper.IssueJwt(new TokenModel
            {
                ID = id,
                Account = "user",
                Name = "admin",
                TenantID = tenant
            });
        }

        /// <summary>
        /// 获取当前登录人信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public CurrentUserInfoModel GetCurrentUserInfo()
        {
            return userInfoHelper.GetCurrentUserInfo();
        }

        /// <summary>
        /// 获取当前登录人使用的Token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public TokenModel GetCurrentToken()
        {
            return userInfoHelper.GetCurrentTokenInfo();
        }

        /// <summary>
        /// 解析Token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public TokenModel SerializeJwt(string token)
        {
            return tokenHelper.SerializeJwt(token);
        }


        /// <summary>
        /// HttpContext
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<TokenModel> TestHttpContextAsync()
        {
            var traceId = HttpContext.TraceIdentifier;
            logger.LogInformation("TraceID: {1}", traceId);
            return await Task.FromResult(default(TokenModel));
        }
    }

}
