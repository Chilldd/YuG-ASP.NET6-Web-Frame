using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Core.Cache
{
    /// <summary>
    /// 缓存配置类
    /// </summary>
    public class CacheOption : IModelCheck
    {
        public static string CacheOptionName = "YuGAppSettings:Cache";

        public CacheType CacheType { get; set; } = CacheType.Redis;

        public string ConnectionString { get; set; }

        public void CheckModel()
        {
            string serverName = SystemServerEnum.Cache.GetEnumDescription();
            if (string.IsNullOrEmpty(ConnectionString))
                throw new YuGCustomizeException($"【{serverName}】缺少配置信息【{CacheOptionName}:ConnectionString】");

        }
    }

    public enum CacheType
    {
        [Description("RedisRepository")]
        Redis = 0
    }
}
