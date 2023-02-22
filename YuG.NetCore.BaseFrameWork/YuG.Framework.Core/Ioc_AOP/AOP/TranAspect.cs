using Castle.DynamicProxy;
using YuG.Framework.ORM;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Core.AOP
{
    public class TranAspect : IInterceptor
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly ILogger<TranAspect> _logger;

        public TranAspect(IUnitOfWork unitofWork, 
                          ILogger<TranAspect> logger)
        {
            this._unitofWork = unitofWork;
            this._logger = logger;
        }

        /// <summary>
        /// 切面处理方法
        /// </summary>
        /// <param name="invocation"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Intercept(IInvocation invocation)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;
            var attrs = method.GetCustomAttributes(true)
                              .FirstOrDefault(x => x.GetType() == typeof(UseTranAttribute));

            if (attrs is UseTranAttribute)
            {
                try
                {
                    //Begin Tran
                    _unitofWork.BeginTran();

                    //Exec Function
                    invocation.Proceed();

                    if (AOPHelper.IsAsyncMethod(invocation.Method))
                    {
                        var res = invocation.ReturnValue;
                        if (res is Task)
                            Task.WaitAll(res as Task);
                    }

                    //Commit
                    _unitofWork.CommitTran();
                }
                catch (Exception ex)
                {
                    //Rollback
                    _unitofWork.RollbackTran();
                    _logger.LogError(ex, $"【{method.Name}】事务提交失败，【{ex.Message}】");
                    throw new YuGCustomizeException(ResultEnum.TranCommitError, ex);
                }
            }
            else
            {
                invocation.Proceed();
            }
        }
    }
}
