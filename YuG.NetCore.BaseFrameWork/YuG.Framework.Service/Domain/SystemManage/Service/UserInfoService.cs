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
using System.Data;

namespace YuG.Framework.Service.Domain.SystemManage
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserInfoService
    {
        private readonly IUserInfoRepository _repository;
        private readonly ILogger<UserInfoService> _logger;
        private readonly IMapper _mapper;
        private readonly IUserInfoHelper _userInfo;

        public UserInfoService(IUserInfoRepository repository,
                                   ILogger<UserInfoService> logger,
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
        public async Task<TbUserEntity> GetDataAsync(int id)
        {
            return await _repository.GetFindByIDAsync<TbUserEntity>(id);
        }

        /// <summary>
        /// Get Data List 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<PageModel<UserInfoVO>> GetListAsync(UserInfoQuery query)
        {
            var data = await _repository.GetPageAsync<TbUserEntity,
                                                      UserInfoVO>(query,
                                                                    e => e.DelFlag == Core.DbEnum.DelFlag.UnDelete);
            return data;
        }

        /// <summary>
        /// Add Data 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ResultObject<int>> AddAsync(AddUserInfoDTO dto)
        {
            var model = new TbUserEntity();
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
        public async Task<ResultObject<object>> EditAsync(EditUserInfoDTO dto)
        {
            var model = await _repository.GetFindByIDAsync<TbUserEntity>(dto.ID);
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
            var model = await _repository.GetFindByIDAsync<TbUserEntity>(id);
            if (model is null)
                return BuildResultObject.BuildCustomizeResult(ResultEnum.DataNotFound, $"ID: 【{ id }】 不存在");

            model.UpdateUser = _userInfo.GetCurrentUserID().ToString();
            model.UpdateTime = DateTime.Now;

            return await _repository.EditDelFlagAsync(model) ?
                         BuildResultObject.BuildSuccessResult() :
                         BuildResultObject.BuildCustomizeResult(ResultEnum.DataSaveFail);
        }

        public async Task<List<TbUserEntity>> Test(string sql)
        {
            return await _repository.AdoExecQuery<TbUserEntity>("select * from tb_user");
        }
        public async Task<Tuple<List<TbUserEntity>, List<TbRoleEntity>>> Test2(string sql)
        {
            return await _repository.AdoExecQuery<TbUserEntity, TbRoleEntity>("select * from tb_user; select * from tb_role");
        }
        public async Task<TbUserEntity> Test3(string sql)
        {
            return await _repository.AdoExecQuerySingle<TbUserEntity>("select * from tb_user;");
        }
        public async Task<DataTable> Test4(string sql)
        {
            DataTable dt = await _repository.AdoExecQueryReturnDT("select * from tb_user;");
            return dt;
        }
        public async Task<int> Test5(string sql)
        {
            return await _repository.AdoExecuteCommand("delete from tb_user where id = 5");
        }
    }
}
