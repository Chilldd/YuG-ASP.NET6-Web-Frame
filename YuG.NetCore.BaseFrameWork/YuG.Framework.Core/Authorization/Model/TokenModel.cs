using YuG.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Authorization
{
    public class TokenModel : IModelCheck
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

        public void CheckModel()
        {
            if (ID is 0)
                throw new YuGCustomizeException($"【TokenModel】ID不能为0");
            if (string.IsNullOrWhiteSpace(Account))
                throw new YuGCustomizeException($"【TokenModel】Accout不能为空");
            if (string.IsNullOrWhiteSpace(Name))
                throw new YuGCustomizeException($"【TokenModel】Name不能为空");
        }
    }
}
