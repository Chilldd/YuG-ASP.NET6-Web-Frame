using YuG.Framework.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.ORM.Sqlsugar
{
    /// <summary>
    /// ORM Sqlsugar
    /// </summary>
    public static class SqlsugarSetup
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(SqlsugarSetup));
        public static void AddSqlsugarSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            //Get Option
            var section = configuration.GetSection(ORMOption.ORMOptionName);
            var option = section.Get<ORMOption>();
            if (option is null)
                throw new YuGCustomizeException($"【{SystemServerEnum.ORM}】缺少配置信息【{ORMOption.ORMOptionName}】");
            option.DbOptions.ForEach(e => e.CheckModel());
            services.Configure<ORMOption>(section);

            var configs = new List<ConnectionConfig>(option.DbOptions.Count);
            option.DbOptions
                  .Where(e => e.Enable)
                  .ToList()
                  .ForEach(e =>
                  {
                      List<SlaveConnectionConfig> slaves = null;
                      if (e.SlaveConnection is not null)
                      {
                          slaves = new List<SlaveConnectionConfig>(e.SlaveConnection.Length);
                          e.SlaveConnection.ToList().ForEach(e => slaves.Add(new SlaveConnectionConfig() { ConnectionString = e }));
                      }
                      configs.Add(new ConnectionConfig
                      {
                          ConfigId = e.ConID,
                          ConnectionString = e.Connection,
                          DbType = e.DbType,
                          IsAutoCloseConnection = e.IsAutoCloseConnection,
                          SlaveConnectionConfigs = slaves,
                          MoreSettings = e.MoreSettings
                      });
                  });

            //Add Sqlsugar ORM
            services.AddScoped<ISqlSugarClient>(e =>
            {
                configs.ForEach(option =>
                 {
                     option.AopEvents = new AopEvents
                     {
                         //SQL Log
                         OnLogExecuting = (sql, pars) =>
                         {
                             foreach (var item in pars)
                             {
                                 sql = sql.Replace(item.ParameterName, item.Value.ObjToTrimString());
                                 _logger.Info(sql);
                             }
                             Console.WriteLine(sql);
                         }
                     };
                 });
                return new SqlSugarScope(configs);
            });
            SystemServerInformationHelper.OpenServerInformation(SystemServerEnum.ORM);
            Console.WriteLine(string.Join("\n", configs.Select(e => $"数据库ID：【{e.ConfigId}】数据库类型：【{e.DbType}】").ToList()));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBaseRepository, BaseRepository>();
        }
    }
}
