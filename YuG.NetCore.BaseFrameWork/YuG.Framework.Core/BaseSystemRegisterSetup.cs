using YuG.Framework.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework
{
    /// <summary>
    /// 系统基础服务注册
    /// </summary>
    public static class BaseSystemRegisterSetup
    {
        public static void AddBaseRegisterSetup(this IServiceCollection services, IConfiguration configuration)
        {
            //Appsetting
            services.AddSingleton(new AppsettingsHelper(configuration));
            //Http
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
