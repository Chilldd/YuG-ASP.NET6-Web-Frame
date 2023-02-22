using System;
using System.Collections.Generic;
using System.Text;

namespace YuG.Framework
{
    /// <summary>
    /// 通用分页查询对象
    /// </summary>
    public class BaseQuery
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageNum { get; set; } = 1;

        /// <summary>
        /// 每页多少条
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 排序字段
        /// </summary>
        public string? Order { get; set; }

        /// <summary>
        /// 正序，倒序
        /// asc：正序
        /// desc：倒序
        /// </summary>
        public string? Desc { get; set; }
    }
}
