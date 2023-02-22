using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace YuG.Framework.Core.ExceptionHandler
{
    /// <summary>
    /// 模型验证处理
    /// </summary>
    public static class ModelVerifySetup
    {
        /// <summary>
        /// Model验证
        /// </summary>
        /// <param name="services"></param>
        public static void AddModelVerifySetup(this IServiceCollection services, IConfiguration configuration)
        {
            //模型绑定特性验证，自定义返回格式
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    string message = "请求参数有误：";
                    List<string> res = new List<string>();
                    foreach (var item in actionContext.ModelState.Values)
                    {
                        res.Add(string.Join("，", item.Errors.Select(e => e.ErrorMessage).ToArray()));
                    }
                    message += string.Join(",", res.ToArray());
                    return new JsonResult(BuildResultObject.BuildCustomizeResult(ResultEnum.BadRequest, message));
                };
            });
            SystemServerInformationHelper.OpenServerInformation(SystemServerEnum.ModelVerify);
        }
    }
}