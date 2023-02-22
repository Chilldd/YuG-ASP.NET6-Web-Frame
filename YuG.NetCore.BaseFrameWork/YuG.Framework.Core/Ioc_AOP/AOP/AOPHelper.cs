using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Core.AOP
{
    /// <summary>
    /// AOP帮助类
    /// </summary>
    public static class AOPHelper
    {
        /// <summary>
        /// 判断方法是不是异步方法
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static bool IsAsyncMethod(MethodInfo method)
        {
            return  method.ReturnType == typeof(Task) || 
                    (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>));
        }
    }
}
