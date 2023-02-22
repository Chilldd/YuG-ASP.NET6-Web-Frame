using System.Collections.Generic;

namespace YuG.Framework.ORM
{
    /// <summary>
    /// 通用分页信息类
    /// </summary>
    public class PageModel<T>
    {
        /// <summary>
        /// 当前页标
        /// </summary>
        public int PageNum { get; set; }

        /// <summary>
        /// 数据总数
        /// </summary>
        public int DataCount { get; set; } = 0;

        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { set; get; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public List<T> Data { get; set; }
    }
}