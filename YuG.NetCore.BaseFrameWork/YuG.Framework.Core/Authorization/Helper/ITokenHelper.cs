using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Authorization
{
    public interface ITokenHelper
    {
        /// <summary>
        /// 颁发token
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns>token</returns>
        string IssueJwt(TokenModel tokenModel);

        /// <summary>
        /// 解析token
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns>token model</returns>
        TokenModel SerializeJwt(string jwtStr);

    }
}
