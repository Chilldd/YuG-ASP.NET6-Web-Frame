using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Core.Ioc
{
    public class IocOption : IModelCheck
    {


        /// <summary>
        /// appsetting.json key
        /// </summary>
        public static string ApplicationOptionName = "YuGAppSettings:Ioc_AOP";

        /// <summary>
        /// IOC容器所有注入的类型查看页面地址
        /// </summary>
        public string QueryAllServiceUrl { get; set; } = "/allservices";

        /// <summary>
        /// 方法数据缓存
        /// </summary>
        public bool MethodDataCache { get; set; }

        /// <summary>
        /// 方法日志
        /// </summary>
        public bool MethodLog { get; set; }

        /// <summary>
        /// 多租户
        /// </summary>
        public bool Tenant { get; set; }

        /// <summary>
        /// 事务管理
        /// </summary>
        public bool Tran { get; set; }

        /// <summary>
        /// 业务层项目名称
        /// </summary>
        public string ServiceDLLName { get; set; }

        /// <summary>
        /// 业务层是否继承接口
        /// </summary>
        public bool IsServiceFromInterface { get; set; }

        /// <summary>
        /// 仓储层项目名称
        /// </summary>
        public string RepositoryDLLName { get; set; }


        public void CheckModel()
        {
            string serverName = SystemServerEnum.IocAOP.GetEnumDescription();
            if (string.IsNullOrWhiteSpace(ServiceDLLName))
                throw new YuGCustomizeException($"【{serverName}】缺少配置信息【{ApplicationOptionName}:ServiceDLLName】");
            if (string.IsNullOrWhiteSpace(RepositoryDLLName))
                throw new YuGCustomizeException($"【{serverName}】缺少配置信息【{ApplicationOptionName}:RepositoryDLLName】");
            if (string.IsNullOrWhiteSpace(QueryAllServiceUrl))
                throw new YuGCustomizeException($"【{serverName}】缺少配置信息【{ApplicationOptionName}:QueryAllServiceUrl】");
        }
    }
}
