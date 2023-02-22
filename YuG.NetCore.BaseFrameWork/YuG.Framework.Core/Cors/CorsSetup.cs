using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace YuG.Framework.Core.Cors
{
    /// <summary>
    /// 添加跨域
    /// </summary>
    public static class CorsSetup
    {
        public static string CorsName = "LimitRequests";
        public static void AddCorsSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var section = configuration.GetSection(CorsOption.CorsOptionName);
            var option = section.Get<CorsOption>();

            //如果Appsetting中没有配置Cors，创建默认策略对象
            if (option is null)
                option = new CorsOption();
            else
                option.CheckModel();
            services.Configure<CorsOption>(section);

            string msg = $"Cors策略名称：【{CorsName}】，Cors策略：";
            Action<CorsPolicyBuilder> configurePolicy = null;

            if (option.IsAllowAnyMethod)
            {
                configurePolicy = policy =>
                                  {
                                      policy.AllowAnyMethod()
                                            .SetIsOriginAllowed(_ => true)
                                            .AllowAnyHeader()
                                            .AllowCredentials();
                                  };
                msg += "【允许任何方法】";
            }
            else
            {
                configurePolicy = policy =>
                                  {
                                      // 支持多个域名端口，注意端口号后不要带/斜杆：比如localhost:8000/，是错的
                                      // 注意，http://127.0.0.1:1818 和 http://localhost:1818 是不一样的，尽量写两个
                                      policy.WithOrigins(option.WhiteList)
                                            //允许所有请求头
                                            .AllowAnyHeader()
                                            //允许所有请求方法
                                            .AllowAnyMethod()
                                            //允许cookie信息
                                            .AllowCredentials();
                                  };
                msg += $"【允许指定端口：{string.Join(',', option.WhiteList)}】";
            }

            //add Cors Server
            services.AddCors(c =>
            {
                c.AddPolicy(CorsName, configurePolicy);
            });

            SystemServerInformationHelper.OpenMiddlewareInformation(SystemServerEnum.Cors, msg);
        }
    }
}
