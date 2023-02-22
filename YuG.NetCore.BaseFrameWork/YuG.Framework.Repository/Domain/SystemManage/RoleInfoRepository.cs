using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using YuG.Framework.ORM;
using YuG.Framework.Service.Domain.SystemManage;

namespace YuG.Framework.Repository.Domain.SystemManage
{
    public class RoleInfoRepository : BaseRepository, IRoleInfoRepository
    {
        public RoleInfoRepository(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
    }
}
