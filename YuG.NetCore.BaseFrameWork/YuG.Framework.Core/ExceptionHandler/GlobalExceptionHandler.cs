using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace YuG.Framework.Core.ExceptionHandler
{
    /// <summary>
    /// 全局异常拦截
    /// </summary>
    public class GlobalExceptionHandler : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<GlobalExceptionHandler> _loggerHelper;

        public GlobalExceptionHandler(IWebHostEnvironment env, ILogger<GlobalExceptionHandler> loggerHelper)
        {
            _env = env;
            _loggerHelper = loggerHelper;
        }

        public void OnException(ExceptionContext context)
        {
            var json = new ErrorResult();
            if (context.Exception is YuGCustomizeException obj)
            {
                if (obj.errorResult is null)
                {
                    json.code = (int)ResultEnum.CustomizeError;
                    json.msg = obj.Message;
                    if (_env.IsDevelopment())
                    {
                        json.details = context.Exception.StackTrace;//堆栈信息
                    }
                }
                else
                    json = obj.errorResult;
            }
            else
            {
                //未捕捉到的错误
                json.code = (int)ResultEnum.Error;
                json.msg = $"{ResultEnum.Error.GetEnumDescription()}。错误详情：{context.Exception.Message}";
                if (_env.IsDevelopment())
                {
                    json.details = context.Exception.StackTrace;//堆栈信息
                }
            }

            context.Result = new InternalServerErrorObjectResult(json);
            _loggerHelper.LogError(WriteLog(json.msg, context.Exception));
        }

        /// <summary>
        /// 自定义返回格式
        /// </summary>
        /// <returns></returns>
        public string WriteLog(string throwMsg, Exception ex)
        {
            return string.Format("\r\n【自定义错误】：{0} \r\n【异常类型】：{1} \r\n【异常信息】：{2} \r\n【堆栈调用】：{3}",
                new object[] {
                    throwMsg,
                    ex.GetType().Name,
                    ex.Message,
                    ex.StackTrace });
        }
    }

    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object value) : base(value)
        {
            StatusCode = StatusCodes.Status200OK;
        }
    }

}