using YuG.Framework.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace YuG.Framework.Authorization
{
    /// <summary>
    /// Jwt帮助类
    /// </summary>
    public class JwtHelper : ITokenHelper
    {
        private readonly ILogger<JwtHelper> _logger;
        private readonly AuthenticationOption _option;

        public JwtHelper(IOptions<AuthenticationOption> options,
                         ILogger<JwtHelper> logger)
        {
            this._logger = logger;
            this._option = options.Value;
        }


        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public string IssueJwt(TokenModel model)
        {
            if(model is null)
                throw new YuGCustomizeException("【颁发Token失败】未能提供所需参数");
            model.CheckModel();

            //jwt参数详解：https://datatracker.ietf.org/doc/html/rfc7519#section-4
            var claims = new List<Claim>
              {
                new Claim(JwtRegisteredClaimNames.Jti, model.ID.ToString()),
                new Claim("Account",model.Account),
                new Claim("Name",model.Name),
                new Claim("TenantID",model.TenantID.ObjToTrimString()),
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(_option.ExpirationTime)).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Iss,_option.Issuer),
                new Claim(JwtRegisteredClaimNames.Aud,_option.Audience)
               };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_option.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(issuer: _option.Issuer, claims: claims, signingCredentials: creds);

            var jwtHandler = new JwtSecurityTokenHandler();
            var encodedJwt = jwtHandler.WriteToken(jwt);

            return encodedJwt;
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public TokenModel SerializeJwt(string jwtStr)
        {
            if (string.IsNullOrWhiteSpace(jwtStr))
                throw new YuGCustomizeException("【无法解析Token】Token为空");

            jwtStr = jwtStr.Replace(AuthenticationConstant.AuthenticationSchemeStr, "");
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken;
            try
            {
                jwtToken = jwtHandler.ReadJwtToken(jwtStr);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "【Token解析失败】");
                throw new YuGCustomizeException("【Token解析失败】请检查Token是否有效");
            }

            return new TokenModel
            {
                ID = jwtToken.Id.ObjToInt(),
                Name = jwtToken.Claims.GetClaimValue("Name"),
                Account = jwtToken.Claims.GetClaimValue("Account"),
                TenantID = jwtToken.Claims.GetClaimValue("TenantID").ObjToInt()
            };
        }
        
    }
}
