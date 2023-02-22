using AutoMapper;
using YuG.Framework.Model;

namespace YuG.Framework.Service.Domain.SystemManage
{
    public class RoleInfoProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public RoleInfoProfile()
        {
            CreateMap<AddRoleInfoDTO, TbRoleEntity>();
            CreateMap<EditRoleInfoDTO, TbRoleEntity>();
            CreateMap<TbRoleEntity, RoleInfoVO>();
        }
    }
}