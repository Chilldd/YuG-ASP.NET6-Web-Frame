using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace YuG.Framework.ORM
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISqlSugarClient _client;
        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWork(ISqlSugarClient sqlSugarClient, ILogger<UnitOfWork> logger)
        {
            _client = sqlSugarClient;
            _logger = logger;
        }

        /// <summary>
        /// 获取DB，保证唯一性
        /// </summary>
        /// <returns></returns>
        public SqlSugarScope GetDbClient()
        {
            return _client as SqlSugarScope;
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        public void BeginTran()
        {
            GetDbClient().BeginTran();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTran()
        {
            try
            {
                GetDbClient().CommitTran();
            }
            catch (Exception ex)
            {
                GetDbClient().RollbackTran();
                _logger.LogError(ex, $"事务提交失败【{ex.Message}】");
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTran()
        {
            GetDbClient().RollbackTran();
        }
    }
}
