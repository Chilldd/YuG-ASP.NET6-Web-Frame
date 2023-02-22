using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using YuG.Framework.ORM;
using Microsoft.Extensions.Logging;
using YuG.Framework.Authorization;
using YuG.Framework.Core;
using YuG.Framework.Model;

namespace YuG.Framework.Service.Domain.SystemManage
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class RoleInfoService
    {
        private readonly IRoleInfoRepository _repository;
        private readonly ILogger<RoleInfoService> _logger;
        private readonly IMapper _mapper;
        private readonly IUserInfoHelper _userInfo;

        public RoleInfoService(IRoleInfoRepository repository,
                                   ILogger<RoleInfoService> logger,
                                   IMapper mapper,
                                   IUserInfoHelper userInfo)
        {
            this._repository = repository;
            this._logger = logger;
            this._mapper = mapper;
            this._userInfo = userInfo;
        }

        /// <summary>
        /// Get Data By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TbRoleEntity> GetDataAsync(int id)
        {
            return await _repository.GetFindByIDAsync<TbRoleEntity>(id);
        }

        /// <summary>
        /// Get Data List 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<PageModel<RoleInfoVO>> GetListAsync(RoleInfoQuery query)
        {
            var data = await _repository.GetPageAsync<TbRoleEntity,
                                                      RoleInfoVO>(query,
                                                                  e => e.DelFlag == Core.DbEnum.DelFlag.UnDelete);
            return data;
        }


        /// <summary>
        /// Add Data 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ResultObject<int>> AddAsync(AddRoleInfoDTO dto)
        {
            var model = new TbRoleEntity();
            _mapper.Map(dto, model);

            model.CreateUser = _userInfo.GetCurrentUserID().ToString();
            model.CreateTime = DateTime.Now;
            model.DelFlag = DbEnum.DelFlag.UnDelete;

            int res = await _repository.AddAsync(model);
            return res > 0 ? BuildResultObject.BuildSuccessResult(res) : BuildResultObject.BuildCustomizeResultData(ResultEnum.DataSaveFail, 0);
        }

        /// <summary>
        /// Edit Data
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ResultObject<object>> EditAsync(EditRoleInfoDTO dto)
        {
            var model = await _repository.GetFindByIDAsync<TbRoleEntity>(dto.ID);
            if (model is null)
                return BuildResultObject.BuildCustomizeResult(ResultEnum.DataNotFound, $"ID: 【{ dto.ID }】 不存在");

            _mapper.Map(dto, model);

            model.UpdateUser = _userInfo.GetCurrentUserID().ToString();
            model.UpdateTime = DateTime.Now;

            return await _repository.EditAsync(model) ?
                         BuildResultObject.BuildSuccessResult() :
                         BuildResultObject.BuildCustomizeResult(ResultEnum.DataSaveFail);
        }

        /// <summary>
        /// Delete Data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResultObject<object>> DeleteAsync(int id)
        {
            var model = await _repository.GetFindByIDAsync<TbRoleEntity>(id);
            if (model is null)
                return BuildResultObject.BuildCustomizeResult(ResultEnum.DataNotFound, $"ID: 【{ id }】 不存在");

            model.UpdateUser = _userInfo.GetCurrentUserID().ToString();
            model.UpdateTime = DateTime.Now;

            return await _repository.EditDelFlagAsync(model) ?
                         BuildResultObject.BuildSuccessResult() :
                         BuildResultObject.BuildCustomizeResult(ResultEnum.DataSaveFail);
        }
    }
}
