using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Core.Cache
{
    public static class CacheSetup
    {
        public static void AddCacheSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            string serverName = SystemServerEnum.Cache.GetEnumDescription();

            //get option
            var section = configuration.GetSection(CacheOption.CacheOptionName);
            var option = section.Get<CacheOption>();
            if (option is null)
                throw new YuGCustomizeException($"【{serverName}】缺少配置信息【{CacheOption.CacheOptionName}】");
            option.CheckModel();
            services.Configure<CacheOption>(section);

            //Add Cache Server
            switch (option.CacheType)
            {
                case CacheType.Redis:
                    services.AddTransient<IRedisRepository, RedisRepository>();
                    services.AddSingleton<ConnectionMultiplexer>(sp =>
                    {
                        var configuration = ConfigurationOptions.Parse(option.ConnectionString, true);
                        configuration.ResolveDns = true;
                        return ConnectionMultiplexer.Connect(configuration);
                    });
                    break;
                default:
                    throw new YuGCustomizeException($"【{serverName}】错误的缓存类型");
            }

            SystemServerInformationHelper.OpenServerInformation(SystemServerEnum.Cache, serverName);
        }
    }
}
