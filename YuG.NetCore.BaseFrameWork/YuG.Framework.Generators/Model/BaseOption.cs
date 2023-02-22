using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Generators
{
    public class BaseOption
    {
        public BaseOption(DbOption dbOption)
        {
            DbOption = dbOption;
        }

        public DbOption DbOption { get; set; }
    }

    public class DbOption
    {
        public DbOption(string connection, DbType dbType)
        {
            Connection = connection;
            DbType = dbType;
        }

        /// <summary>
        /// 数据库连接地址
        /// </summary>
        public string Connection { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public SqlSugar.DbType DbType { get; set; }
    }
}
