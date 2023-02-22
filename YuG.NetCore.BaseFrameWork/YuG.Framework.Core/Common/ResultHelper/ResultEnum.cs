using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Core
{
    public enum ResultEnum
    {

        [Description("系统配置错误")]
        SystemOptionError = 101,

        [Description("成功")]
        Success = 200,

        [Description("请求报文语法错误或参数错误")]
        BadRequest = 400,

        [Description("认证失败")]
        NotAuth = 401,

        [Description("授权失败")]
        NotAPIAuth = 403,

        [Description("未找到访问资源")]
        NotFound = 404,

        [Description("后台系统报错")]
        Error = 500,

        [Description("后台系统异常")]
        CustomizeError = 1001,

        [Description("ORM异常")]
        ORMError = 1002,

        [Description("事务提交失败")]
        TranCommitError = 1003,

        [Description("数据不存在")]
        DataNotFound = 1004,

        [Description("数据保存失败")]
        DataSaveFail = 1005
    }
    
    public static class ResultEnumExtensions
    {
        public static string GetEnumDescription(this Enum value)
        {
            string name = value.ToString();
            FieldInfo fieldInfo = value.GetType().GetField(name);
            DescriptionAttribute descriptionAttribute = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            if (descriptionAttribute != null)
            {
                return descriptionAttribute.Description;
            }
            return string.Empty;
        }
    }
}
