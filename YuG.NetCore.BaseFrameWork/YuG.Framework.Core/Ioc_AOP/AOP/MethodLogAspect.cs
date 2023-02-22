using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Core.AOP
{
    /// <summary>
    /// 方法日志切面
    /// </summary>
    public class MethodLogAspect : IInterceptor
    {
        private readonly IHttpContextAccessor httpContext;
        private readonly ILogger<MethodLogAspect> log;

        public MethodLogAspect(IHttpContextAccessor httpContext,
                               ILogger<MethodLogAspect> log)
        {
            this.httpContext = httpContext;
            this.log = log;
        }

        /// <summary>
        /// 日志切面
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {
            string UserName = httpContext.HttpContext?.User?.Identity?.Name;

            StringBuilder logStr = new StringBuilder();
            logStr.Append($"【当前操作人】: {UserName ?? "当前无登录人"} \r\n");
            logStr.Append($"【当前执行方法】: {invocation.Method.Name} \r\n");
            logStr = logStr.Append($"【参数】: {string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray())} \r\n");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {

                invocation.Proceed();

                if (AOPHelper.IsAsyncMethod(invocation.Method))
                {
                    //获取返回值
                    var result = invocation.ReturnValue;
                    if (result is Task)
                    {
                        //等待方法执行完成
                        Task.WaitAll(result as Task);
                    }
                }
                stopwatch.Stop();
                logStr.Append($"【方法执行成功】\r\n");
                logStr.Append($"【执行时间】: {stopwatch.Elapsed} ");
                SaveSuccessLog(logStr.ToString());
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                logStr.Append($"【方法执行失败】\r\n");
                logStr.Append($"【执行时间】: {stopwatch.Elapsed} ");
                SaveErrorLog(logStr.ToString(), ex);

                if (ex is YuGCustomizeException)
                    throw;
            }
        }

        /// <summary>
        /// 保存方法调用成功日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        private void SaveSuccessLog(string msg)
        {
            log.LogInformation(msg);
        }

        /// <summary>
        /// 保存方法调用失败日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        private void SaveErrorLog(string msg, Exception ex)
        {
            log.LogError(ex, msg);
        }
    }
}
