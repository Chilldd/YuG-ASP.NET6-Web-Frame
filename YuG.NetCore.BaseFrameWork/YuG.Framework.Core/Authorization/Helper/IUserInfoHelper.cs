using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Authorization
{
    public interface IUserInfoHelper
    {
        /// <summary>
        /// 获取当前用户是否通过认证
        /// </summary>
        /// <returns></returns>
        bool IsAuthenticated();

        /// <summary>
        /// 获取当前登录人信息
        /// </summary>
        /// <returns></returns>
        CurrentUserInfoModel GetCurrentUserInfo();

        /// <summary>
        /// 获取当前登录人ID
        /// </summary>
        /// <returns></returns>
        int GetCurrentUserID();

        /// <summary>
        /// 获取当前请求中的token
        /// </summary>
        /// <returns></returns>
        string GetCurrentToken();

        /// <summary>
        /// 获取当前请求中的token，并解析
        /// </summary>
        /// <returns></returns>
        TokenModel GetCurrentTokenInfo();
    }
}
