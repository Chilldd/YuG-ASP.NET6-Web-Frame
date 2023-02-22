using YuG.Framework.Generators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YuG.Framework.WebAPI.Controllers
{
    /// <summary>
    /// 代码生成
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GeneratorsController : ControllerBase
    {

        /// <summary>
        /// 生成实体
        /// </summary>
        /// <returns></returns>
        [HttpGet("entity")]
        public IActionResult BuildEntity()
        {
            var dbOption = new DbOption("Data Source=D:\\Work\\File\\SqliteDb\\fisk_framework_dev.db", SqlSugar.DbType.Sqlite);
            var option = new BuildEntityOption("YuG.Framework.Test", @"D:\Work\Code\NETWorkSpace\YuG.NetCore.BaseFrameWork\YuG.Framework.Model\Test\", dbOption);
            option.EntityInfos = new List<EntityInfo>();
            option.IsExtend = true;
            DbFirstHelper.BuildEntity(option);

            return Ok();
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <returns></returns>
        [HttpGet("service")]
        public void BuildServiceCode()
        {
            var dbOption = new DbOption("Data Source=D:\\Work\\File\\SqliteDb\\fisk_framework_dev.db", SqlSugar.DbType.Sqlite);
            var option = new BuildServiceCodeOption(dbOption);
            DbFirstHelper.BuildServiceCode(option);
        }
    }
}
