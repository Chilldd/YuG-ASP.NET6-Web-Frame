using Castle.DynamicProxy;
using YuG.Framework.Authorization;
using YuG.Framework.ORM;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Core.AOP
{
    /// <summary>
    /// 多租户切面
    /// </summary>
    public class TenantAspect : IInterceptor
    {
        private readonly IUserInfoHelper _userHelper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ORMOption _options;

        public TenantAspect(IUserInfoHelper userHelper,
                            IUnitOfWork unitOfWork,
                            IOptions<ORMOption> options)
        {
            this._userHelper = userHelper;
            this._unitOfWork = unitOfWork;
            this._options = options.Value;
        }

        /// <summary>
        /// 切面
        /// </summary>
        /// <param name="invocation"></param>
        /// <exception cref="YuGCustomizeException"></exception>
        public void Intercept(IInvocation invocation)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;
            var attrs = method.GetCustomAttributes(true)
                              .FirstOrDefault(x => x.GetType() == typeof(TenantAttribute));

            if (attrs is TenantAttribute)
            {
                //Check
                var userInfo = _userHelper.GetCurrentUserInfo();
                if (userInfo is null)
                    throw new YuGCustomizeException("当前未获取到登录人信息");
                if (userInfo.TenantID is null)
                    throw new YuGCustomizeException("当前登录人没有租户ID");
                var option = _options.DbOptions.Where(e => e.ConID == userInfo.TenantID).FirstOrDefault();
                if (option is null)
                    throw new YuGCustomizeException("未知的租户ID");

                //Get Db Client
                var client = _unitOfWork.GetDbClient();
                //Switch Db
                client.ChangeDatabase(option.ConID);

                invocation.Proceed();
            }
            else
            {
                invocation.Proceed();
            }
        }
    }
}
