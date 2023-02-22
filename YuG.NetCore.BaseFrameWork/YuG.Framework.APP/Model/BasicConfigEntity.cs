using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.APP.Model
{
    [SqlSugar.SugarTable("basic_config")]
    public class BasicConfigEntity
    {

        [SugarColumn(IsIdentity = true,
            IsPrimaryKey = true,
            ColumnName = "id")]
        public int ID { get; set; }

        [SugarColumn(
            ColumnName = "name",
            ColumnDataType = "varchar",
            Length = 50,
            ColumnDescription = "连接名称",
            IsNullable = true,
            UniqueGroupNameList = new string[] { "name_unique_name" })]
        public string Name { get; set; }

        [SugarColumn(
            ColumnName = "connection",
            ColumnDataType = "varchar",
            Length = 200,
            ColumnDescription = "数据库连接字符串",
            IsNullable = true)]
        public string Connection { get; set; }

        [SugarColumn(
            ColumnName = "dbtype",
            ColumnDataType = "int",
            ColumnDescription = "数据库类型",
            IsNullable = true)]
        public int DbType { get; set; }
    }
}
