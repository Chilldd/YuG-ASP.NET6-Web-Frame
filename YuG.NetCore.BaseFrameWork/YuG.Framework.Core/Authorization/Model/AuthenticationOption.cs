using YuG.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Authorization
{
    /// <summary>
    /// 身份认证配置
    /// </summary>
    public class AuthenticationOption : IModelCheck
    {
        public static string AuthenticationOptionName = "YuGAppSettings:Authentication";

        /// <summary>
        /// 过期时间(默认30分钟)
        /// </summary>
        public int ExpirationTime { get; set; } = 30;

        /// <summary>
        /// Jwt-订阅人
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Jwt-发行人
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Jwt-秘钥
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// 自定义授权处理程序(完整的命名空间+类名)
        /// </summary>
        public string CustomizeAuthorizationHandlerName { get; set; }

        /// <summary>
        /// 自定义授权处理程序所在的DLL文件名称
        /// </summary>
        public string CustomizeAuthorizationHandlerDLLName { get; set; }

        public void CheckModel()
        {
            string serverName = SystemServerEnum.Authorization.GetEnumDescription();
            if (string.IsNullOrWhiteSpace(Audience))
                throw new YuGCustomizeException($"【{serverName}】缺少配置信息【{AuthenticationOptionName}:Authentication:Audience】");
            if (string.IsNullOrWhiteSpace(Issuer))
                throw new YuGCustomizeException($"【{serverName}】缺少配置信息【{AuthenticationOptionName}:Authentication:Issuer】");
            if (string.IsNullOrWhiteSpace(Secret))
                throw new YuGCustomizeException($"【{serverName}】缺少配置信息【{AuthenticationOptionName}:Authentication:Secret】");
            if (ExpirationTime == 0)
                throw new YuGCustomizeException($"【{serverName}】缺少配置信息【{AuthenticationOptionName}:Authentication:ExpirationTime】");

        }
    }
}
