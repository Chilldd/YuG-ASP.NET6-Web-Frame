using System;
using System.Collections.Generic;
using System.Text;

namespace YuG.Framework.Core
{
    /// <summary>
    /// 后端返回对象
    /// </summary>
    public static class BuildResultObject
    {
        /// <summary>
        /// 成功
        /// </summary>
        /// <returns></returns>
        public static ResultObject<object> BuildSuccessResult() => new ResultObject<object>
        {
            code = (int)ResultEnum.Success,
            msg = ResultEnum.Success.GetEnumDescription(),
            data = null
        };

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="msg">描述信息</param>
        /// <returns></returns>
        public static ResultObject<object> BuildSuccessResult(string msg) => new ResultObject<object>
        {
            code = (int)ResultEnum.Success,
            msg = msg,
            data = null
        };

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="msg">描述信息</param>
        /// <returns></returns>
        public static ResultObject<T> BuildSuccessResult<T>(T data) => new ResultObject<T>
        {
            code = (int)ResultEnum.Success,
            msg = ResultEnum.Success.GetEnumDescription(),
            data = data
        };

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="msg">描述信息</param>
        /// <returns></returns>
        public static ResultObject<T> BuildSuccessResult<T>(string msg, T data) => new ResultObject<T>
        {
            code = (int)ResultEnum.Success,
            msg = msg,
            data = data
        };

        /// <summary>
        /// 失败
        /// </summary>
        /// <returns></returns>
        public static ResultObject<object> BuildErrorResult() => new ResultObject<object>
        {
            code = (int)ResultEnum.Error,
            msg = ResultEnum.Error.GetEnumDescription(),
            data = null
        };

        /// <summary>
        /// 失败
        /// </summary>
        /// <returns></returns>
        public static ResultObject<object> BuildErrorResult(string msg) => new ResultObject<object>
        {
            code = (int)ResultEnum.Error,
            msg = msg,
            data = null
        };

        /// <summary>
        /// 失败
        /// </summary>
        /// <returns></returns>
        public static ResultObject<T> BuildErrorResult<T>(string msg, T data) => new ResultObject<T>
        {
            code = (int)ResultEnum.Error,
            msg = msg,
            data = data
        };

        /// <summary>
        /// 自定义
        /// </summary>
        /// <returns></returns>
        public static ResultObject<object> BuildCustomizeResult(int code, string msg) => new ResultObject<object>
        {
            code = code,
            msg = msg,
            data = null
        };

        /// <summary>
        /// 自定义
        /// </summary>
        /// <returns></returns>
        public static ResultObject<T> BuildCustomizeResult<T>(int code, string msg, T data) => new ResultObject<T>
        {
            code = code,
            msg = msg,
            data = data
        };

        /// <summary>
        /// 自定义
        /// </summary>
        /// <returns></returns>
        public static ResultObject<object> BuildCustomizeResult(ResultEnum @enum) => new ResultObject<object>
        {
            code = (int)@enum,
            msg = @enum.GetEnumDescription(),
            data = null
        };

        /// <summary>
        /// 自定义
        /// </summary>
        /// <returns></returns>
        public static ResultObject<object> BuildCustomizeResult(ResultEnum @enum, string msg) => new ResultObject<object>
        {
            code = (int)@enum,
            msg = msg,
            data = null
        };

        /// <summary>
        /// 自定义
        /// </summary>
        /// <returns></returns>
        public static ResultObject<T> BuildCustomizeResultData<T>(ResultEnum @enum, T data) => new ResultObject<T>
        {
            code = (int)@enum,
            msg = @enum.GetEnumDescription(),
            data = data
        };
    }
}
