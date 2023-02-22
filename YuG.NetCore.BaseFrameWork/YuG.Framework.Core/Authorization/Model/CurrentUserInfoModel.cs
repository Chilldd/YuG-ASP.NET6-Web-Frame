using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Authorization
{
    /// <summary>
    /// 当前登录人
    /// </summary>
    public class CurrentUserInfoModel
    {
        /// <summary>
        /// 登录人id
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 登录人账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 登录人名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 租户id
        /// </summary>
        public int? TenantID { get; set; }
    }
}
