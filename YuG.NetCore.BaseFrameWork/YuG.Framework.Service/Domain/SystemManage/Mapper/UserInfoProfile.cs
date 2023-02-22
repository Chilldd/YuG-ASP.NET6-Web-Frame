using AutoMapper;
using YuG.Framework.Model;

namespace YuG.Framework.Service.Domain.SystemManage
{
    public class UserInfoProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public UserInfoProfile()
        {
            CreateMap<AddUserInfoDTO, TbUserEntity>();
            CreateMap<EditUserInfoDTO, TbUserEntity>();
            CreateMap<TbUserEntity, UserInfoVO>();
        }
    }
}