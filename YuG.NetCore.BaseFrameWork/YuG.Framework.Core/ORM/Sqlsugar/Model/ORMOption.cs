using YuG.Framework.Core;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.ORM
{
    public class ORMOption
    {
        public static string ORMOptionName = "YuGAppSettings:ORM";

        public List<SqlsugarOption> DbOptions { get; set; }
    }

    public class SqlsugarOption : IModelCheck
    {

        /// <summary>
        /// 数据库连接id(用来做动态切换使用)
        /// </summary>
        public int? ConID { get; set; }

        /// <summary>
        /// 数据库类型(如果为null，默认mysql)
        /// </summary>
        public DbType DbType { get; set; } = DbType.MySql;

        /// <summary>
        /// 是否开启此连接(默认启用)
        /// </summary>
        public bool Enable { get; set; } = true;

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string Connection { get; set; }

        /// <summary>
        /// 是否自动关闭连接
        /// 自动释放和关闭数据库连接，如果有事务,事务结束时关闭，否则每次操作后关闭	
        /// </summary>
        public bool IsAutoCloseConnection { get; set; } = true;

        /// <summary>
        /// 从库连接字符串
        /// </summary>
        public string[] SlaveConnection { get; set; }

        /// <summary>
        /// 额外配置
        /// </summary>
        public ConnMoreSettings MoreSettings { get; set; }


        public void CheckModel()
        {
            string serverName = SystemServerEnum.ORM.GetEnumDescription();
            if (string.IsNullOrWhiteSpace(Connection))
                throw new YuGCustomizeException($"【{serverName}】缺少配置信息【{ORMOption.ORMOptionName}:Connection】");
            if (ConID is null)
                throw new YuGCustomizeException($"【{serverName}】缺少配置信息【{ORMOption.ORMOptionName}:ConID】");
        }
    }

}
