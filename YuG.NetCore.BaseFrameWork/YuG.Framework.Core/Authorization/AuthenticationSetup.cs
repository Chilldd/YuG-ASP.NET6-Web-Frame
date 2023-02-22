using Castle.Core.Logging;
using YuG.Framework.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Authorization
{
    /// <summary>
    /// 身份认证服务
    /// </summary>
    public static class AuthenticationSetup
    {
        public static void AddAuthenticationSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            string serverName = SystemServerEnum.Authorization.GetEnumDescription();
            //get option
            var section = configuration.GetSection(AuthenticationOption.AuthenticationOptionName);
            var option = section.Get<AuthenticationOption>();
            if (option is null)
                throw new YuGCustomizeException($"【{serverName}】缺少配置信息【{AuthenticationOption.AuthenticationOptionName}】");
            option.CheckModel();
            services.Configure<AuthenticationOption>(section);

            SystemServerInformationHelper.OpenServerInformation(SystemServerEnum.Authorization);

            //build token params
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(option.Secret));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = option.Issuer,
                ValidateAudience = true,
                ValidAudience = option.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(option.ExpirationTime),
                RequireExpirationTime = true,
            };

            //add jwt
            services.AddAuthorization(o => { })
                    .AddAuthentication(o =>
                    {
                            // 开启Bearer认证
                            o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                            //身份认证处理程序
                            o.DefaultChallengeScheme = nameof(YuGDefaultAuthenticationHandler);
                        o.DefaultForbidScheme = nameof(YuGDefaultAuthenticationHandler);
                    })
                    .AddJwtBearer(o =>
                    {
                            // 添加JwtBearer服务
                            o.TokenValidationParameters = tokenValidationParameters;
                    })
                    .AddScheme<AuthenticationSchemeOptions, YuGDefaultAuthenticationHandler>
                               (nameof(YuGDefaultAuthenticationHandler),
                               o => { });


            // 注册授权处理程序
            if (string.IsNullOrWhiteSpace(option.CustomizeAuthorizationHandlerName) ||
                string.IsNullOrWhiteSpace(option.CustomizeAuthorizationHandlerDLLName))
            {
                services.AddScoped<IAuthorizationHandler, YuGDefaultAuthorizationHandler>();
                Console.WriteLine($"【{serverName}】当前授权处理程序【{nameof(YuGDefaultAuthorizationHandler)}】");
            }
            else
            {
                Type type = null;
                try
                {
                    //获取自定义授权处理程序所在的程序集
                    var servicesDllFile = Path.Combine(AppContext.BaseDirectory, option.CustomizeAuthorizationHandlerDLLName);
                    Assembly assembly = Assembly.LoadFrom(servicesDllFile);
                    //加载自定义授权处理程序类型
                    type = assembly.GetType(option.CustomizeAuthorizationHandlerName);
                }
                catch (Exception ex)
                {
                    throw new YuGCustomizeException($"【{serverName}】加载自定义授权处理程序失败，请检查配置是否正确", ex);
                }

                if (type is null ||
                    !typeof(AuthorizationHandler<IAuthorizationRequirement>).IsAssignableFrom(type))
                {
                    services.AddScoped<IAuthorizationHandler, YuGDefaultAuthorizationHandler>();
                    Console.WriteLine($"【{serverName}】当前授权处理程序【{nameof(YuGDefaultAuthorizationHandler)}】");
                }
                else
                {
                    services.AddScoped(typeof(IAuthorizationHandler), type);
                    Console.WriteLine($"【{serverName}】当前授权处理程序【{type.FullName}】");
                }
            }

            // 注入Jwt操作类
            services.AddTransient<ITokenHelper, JwtHelper>();
            // 注入操作类，获取http请求中token信息
            services.AddTransient<IUserInfoHelper, CurrentUserInfoHelper>();
        }
    }
}
