namespace YuG.Framework.Core
{
    /// <summary>
    /// 异常model
    /// </summary>
    public class ErrorResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 详情
        /// </summary>
        public string details { get; set; }
    }
}
