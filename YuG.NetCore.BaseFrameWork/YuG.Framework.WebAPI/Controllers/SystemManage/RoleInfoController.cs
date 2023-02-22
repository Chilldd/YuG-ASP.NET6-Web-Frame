using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YuG.Framework.Service.Domain.SystemManage;

namespace YuG.Framework.WebAPI.SystemManage
{
    /// <summary>
    /// 角色管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RoleInfoController : ControllerBase
    {
        private readonly RoleInfoService _service;

        /// <summary>
        /// init
        /// </summary>
        public RoleInfoController(RoleInfoService service)
        {
            this._service = service;
        }

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> List([FromQuery] RoleInfoQuery query)
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
        public async Task<IActionResult> Add([FromBody] AddRoleInfoDTO dto)
        {
            return Ok(await _service.AddAsync(dto));
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] EditRoleInfoDTO dto)
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
    }
}