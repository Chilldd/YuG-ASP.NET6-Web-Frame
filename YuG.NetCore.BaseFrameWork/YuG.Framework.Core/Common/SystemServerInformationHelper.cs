using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Core
{
    public static class SystemServerInformationHelper
    {
        public static void OpenMiddlewareInformation(SystemServerEnum @enum, string msg = "")
        {
            ConsoleHelper.WriteSuccessLine($"【{@enum.GetEnumDescription()}】中间件已开启。{msg}");
        }

        public static void OpenServerInformation(SystemServerEnum @enum, string msg = "")
        {
            ConsoleHelper.WriteSuccessLine($"【{@enum.GetEnumDescription()}】服务已开启。{msg}");
        }
        public static void UnOpenMiddlewareInformation(SystemServerEnum @enum, string msg = "")
        {
            ConsoleHelper.WriteWarningLine($"【{@enum.GetEnumDescription()}】中间件未开启。{msg}");
        }

        public static void UnOpenServerInformation(SystemServerEnum @enum, string msg = "")
        {
            ConsoleHelper.WriteWarningLine($"【{@enum.GetEnumDescription()}】服务未开启。{msg}");
        }
    }
}
