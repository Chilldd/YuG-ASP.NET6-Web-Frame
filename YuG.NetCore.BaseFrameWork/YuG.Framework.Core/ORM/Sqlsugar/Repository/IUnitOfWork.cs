using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace YuG.Framework.ORM
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// 获取DB连接
        /// </summary>
        /// <returns></returns>
        SqlSugarScope GetDbClient();

        /// <summary>
        /// 开启事务
        /// </summary>
        void BeginTran();

        /// <summary>
        /// 提交事务
        /// </summary>
        void CommitTran();

        /// <summary>
        /// 回滚事务
        /// </summary>
        void RollbackTran();
    }
}
