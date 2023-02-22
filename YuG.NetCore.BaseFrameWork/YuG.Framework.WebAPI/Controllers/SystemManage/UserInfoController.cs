using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YuG.Framework.Service.Domain.SystemManage;

namespace YuG.Framework.WebAPI.SystemManage
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly UserInfoService _service;

        /// <summary>
        /// init
        /// </summary>
        public UserInfoController(UserInfoService service)
        {
            this._service = service;
        }

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> List([FromQuery] UserInfoQuery query)
        {
            return Ok(BuildResultObject.BuildSuccessResult(await _service.GetListAsync(query)));
        }

        /// <summary>
        /// 根据id查询数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(BuildResultObject.BuildSuccessResult(await _service.GetDataAsync(id)));
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddUserInfoDTO dto)
        {
            return Ok(await _service.AddAsync(dto));
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] EditUserInfoDTO dto)
        {
            return Ok(await _service.EditAsync(dto));
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }

        /// <summary>
        /// test
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("test")]
        public async Task<IActionResult> Test(string sql)
        {
            return Ok(await _service.Test(sql));
        }

        /// <summary>
        /// test2
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("test2")]
        public async Task<IActionResult> Test2(string sql)
        {
            return Ok(await _service.Test2(sql));
        }

        /// <summary>
        /// test3
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("test3")]
        public async Task<IActionResult> Test3(string sql)
        {
            return Ok(await _service.Test3(sql));
        }

        /// <summary>
        /// test4
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("test4")]
        public async Task<IActionResult> Test4(string sql)
        {
            return Ok(await _service.Test4(sql));
        }

        /// <summary>
        /// test5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("test5")]
        public async Task<IActionResult> Test5(string sql)
        {
            return Ok(await _service.Test5(sql));
        }
    }
}