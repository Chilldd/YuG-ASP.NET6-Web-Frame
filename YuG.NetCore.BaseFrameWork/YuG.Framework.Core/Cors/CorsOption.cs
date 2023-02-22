using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Core.Cors
{
    public class CorsOption : IModelCheck
    {
        public static string CorsOptionName = "YuGAppSettings:Cors";

        /// <summary>
        /// 是否所有方法都放行
        /// </summary>
        public bool IsAllowAnyMethod { get; set; } = true;

        /// <summary>
        /// 白名单地址
        /// </summary>
        public string[] WhiteList { get; set; }

        public void CheckModel()
        {
            if (!IsAllowAnyMethod &&
                (WhiteList is null || WhiteList.Length == 0))
                throw new YuGCustomizeException($"【{SystemServerEnum.Cors.GetEnumDescription()}】配置异常，当前没有开启任意请求放行，并且白名单列表为空");
        }
    }
}
