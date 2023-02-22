using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace YuG.Framework.Core.ExceptionHandler
{
    public class ExceptionHandlerSetup
    {
        public static void Interceptor(MvcOptions mvcOptions, IConfiguration configuration)
        {
            //全局异常拦截
            mvcOptions.Filters.Add(typeof(GlobalExceptionHandler));

            SystemServerInformationHelper.OpenServerInformation(SystemServerEnum.Exception);
        }
    }
}
