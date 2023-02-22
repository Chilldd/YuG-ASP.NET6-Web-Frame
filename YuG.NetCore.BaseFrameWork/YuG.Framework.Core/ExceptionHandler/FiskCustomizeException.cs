using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace YuG.Framework.Core
{
    /// <summary>
    /// 自定义异常
    /// </summary>
    public class YuGCustomizeException : Exception
    {
        public ErrorResult errorResult;

        public YuGCustomizeException()
        {
        }

        public YuGCustomizeException(string msg) : base(msg)
        {
        }

        public YuGCustomizeException(string msg, Exception innerException) : base(msg, innerException)
        {
        }

        public YuGCustomizeException(ResultEnum @enum, string msg) : base(JsonConvert.SerializeObject(BuildResultObject.BuildCustomizeResult(@enum, msg)))
        {
            this.errorResult = new ErrorResult()
            {
                code = (int)@enum,
                msg = msg
            };
        }

        public YuGCustomizeException(ResultEnum @enum, Exception innerException) : base(JsonConvert.SerializeObject(BuildResultObject.BuildCustomizeResult(@enum)), innerException)
        {
            this.errorResult = new ErrorResult()
            {
                code = (int)@enum,
                msg = @enum.GetEnumDescription(),
                details = innerException.InnerException.StackTrace
            };
        }

        public YuGCustomizeException(ErrorResult error) : base(JsonConvert.SerializeObject(error))
        {
            this.errorResult = error;
        }

        public YuGCustomizeException(ErrorResult error, Exception innerException) : base(JsonConvert.SerializeObject(error), innerException)
        {
            this.errorResult = error;
        }
    }
}
