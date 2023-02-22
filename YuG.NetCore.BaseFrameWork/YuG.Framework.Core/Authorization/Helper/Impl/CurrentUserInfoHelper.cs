using YuG.Framework.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging; 
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Authorization
{
    public class CurrentUserInfoHelper : IUserInfoHelper
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ITokenHelper _tokenHelper;

        public CurrentUserInfoHelper(IHttpContextAccessor accessor,
                                     ITokenHelper tokenHelper)
        {
            _accessor = accessor;
            _tokenHelper = tokenHelper;
        }



        /// <summary>
        /// 获取当前用户是否通过认证
        /// </summary>
        /// <returns></returns>
        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        /// <summary>
        /// 获取当前登录人信息
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public CurrentUserInfoModel GetCurrentUserInfo()
        {
            if (IsAuthenticated())
            {
                var claims = _accessor.HttpContext.User.Claims.ToList();
                var tenant = claims.Where(e => e.Type == "TenantID").FirstOrDefault()?.Value;
                return new CurrentUserInfoModel
                {
                    ID = (claims.Where(e => e.Type == "jti").FirstOrDefault()?.Value).ObjToInt(),
                    Account = (claims.Where(e => e.Type == "Account").FirstOrDefault()?.Value).ObjToTrimString(),
                    Name = (claims.Where(e => e.Type == "Name").FirstOrDefault()?.Value).ObjToTrimString(),
                    TenantID = string.IsNullOrWhiteSpace(tenant) ? null : tenant.ObjToInt()
                };
            }
            return null;
        }

        /// <summary>
        /// 获取当前登录人ID
        /// </summary>
        /// <returns></returns>
        public int GetCurrentUserID()
        {
            if (IsAuthenticated())
            {
                var claims = _accessor.HttpContext.User.Claims.ToList();
                return (claims.Where(e => e.Type == "jti").FirstOrDefault()?.Value).ObjToInt();
            }
            return 0;
        }


        /// <summary>
        /// 获取当前请求中的token
        /// </summary>
        /// <returns></returns>
        public string GetCurrentToken()
        {
            return _accessor.HttpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, AuthenticationConstant.TokenName).Result;
        }

        /// <summary>
        /// 获取当前请求中的token，并解析
        /// </summary>
        /// <returns></returns>
        public TokenModel GetCurrentTokenInfo()
        {
            return _tokenHelper.SerializeJwt(GetCurrentToken());
        }
    }
}
