using YuG.Framework.Core;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.ORM
{
    public class BaseStatusEntity : BaseEntity
    {
        /// <summary>
        /// 状态
        /// </summary>
        [SqlSugar.SugarColumn(
            ColumnName = "status",
            ColumnDataType = "int",
            Length = 1,
            ColumnDescription = "状态",
            IsNullable = true)]
        public DbEnum.Status? Status { get; set; }
    }
}
