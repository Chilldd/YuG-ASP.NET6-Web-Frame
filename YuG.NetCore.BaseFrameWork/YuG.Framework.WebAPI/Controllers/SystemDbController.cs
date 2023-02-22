using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace YuG.Framework.WebAPI.Controllers
{
    /// <summary>
    /// 系统库处理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SystemDbController : ControllerBase
    {
        private readonly ISqlSugarClient _client;

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="client"></param>
        public SystemDbController(ISqlSugarClient client)
        {
            this._client = client;
        }

        /// <summary>
        /// 创建数据库
        /// </summary>
        [HttpGet("createDb")]
        public void CreateDb()
        {
            _client.DbMaintenance.CreateDatabase();
        }

        /// <summary>
        /// 创建表
        /// </summary>
        [HttpGet("createTable")]
        public void CreateTable()
        {

        }
    }
}
