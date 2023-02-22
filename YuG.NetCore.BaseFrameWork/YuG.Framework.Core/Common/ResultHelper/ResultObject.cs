using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Core
{
    /// <summary>
    /// 后端接口返回实体
    /// </summary>
    public class ResultObject<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public T data { get; set; }
    }
}
