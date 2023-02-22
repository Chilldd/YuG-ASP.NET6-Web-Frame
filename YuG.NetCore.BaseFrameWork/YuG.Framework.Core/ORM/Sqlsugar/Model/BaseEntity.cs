using YuG.Framework.Core;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework
{
    public class BaseEntity
    {
        [SugarColumn(IsIdentity = true,
            IsPrimaryKey = true,
            ColumnName = "id",
            ColumnDataType = "int")]
        public int ID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SqlSugar.SugarColumn(
            ColumnName = "create_time",
            ColumnDataType = "datetime",
            ColumnDescription = "创建时间",
            IsNullable = true)]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [SqlSugar.SugarColumn(
            ColumnName = "create_user",
            ColumnDataType = "varchar",
            Length = 20,
            ColumnDescription = "创建人",
            IsNullable = true)]
        public string? CreateUser { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [SqlSugar.SugarColumn(
            ColumnName = "update_time",
            ColumnDataType = "datetime",
            ColumnDescription = "更新时间",
            IsNullable = true)]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        [SqlSugar.SugarColumn(
            ColumnName = "update_user",
            ColumnDataType = "varchar",
            Length = 20,
            ColumnDescription = "更新人",
            IsNullable = true)]
        public string? UpdateUser { get; set; }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        [SqlSugar.SugarColumn(
            ColumnName = "del_flag",
            ColumnDataType = "int",
            Length = 1,
            ColumnDescription = "逻辑删除",
            IsNullable = true)]
        public DbEnum.DelFlag? DelFlag { get; set; }
    }
}
