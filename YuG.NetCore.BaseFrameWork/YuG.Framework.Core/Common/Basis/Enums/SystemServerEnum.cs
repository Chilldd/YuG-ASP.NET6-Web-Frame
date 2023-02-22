using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Core
{
    public enum SystemServerEnum
    {
        [Description("Ioc容器/AOP切面")]
        IocAOP,
        [Description("Ioc容器查看")]
        QueryIocType,
        [Description("Cors")]
        Cors,
        [Description("全局异常拦截")]
        Exception,
        [Description("身份认证")]
        Authorization,
        [Description("模型验证")]
        ModelVerify,
        [Description("Swagger")]
        Swagger,
        [Description("ORM")]
        ORM,
        [Description("Cache")]
        Cache
    }
}
