using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Generators
{
    public class BuildServiceCodeOption : BaseOption
    {
        public BuildServiceCodeOption(DbOption dbOption) : base(dbOption)
        {
        }

        /// <summary>
        /// 解决方案目录
        /// </summary>
        public string BasePath { get; set; }

        /// <summary>
        /// API层命名空间
        /// </summary>
        public string ControllerNameSpace { get; set; }

        /// <summary>
        /// Service层命名空间
        /// </summary>
        public string ServiceNameSpace { get; set; }

        /// <summary>
        /// 仓储层命名空间
        /// </summary>
        public string RepositoryNameSpace { get; set; }

        /// <summary>
        /// 业务领域名称
        /// </summary>
        public string DomainName { get; set; }

        /// <summary>
        /// 业务层名称（可为空）
        /// </summary>
        public string? ServiceName { get; set; }

        /// <summary>
        /// 仓储层名称（可为空）
        /// </summary>
        public string? RepositoryName { get; set; }

        /// <summary>
        /// 业务描述
        /// </summary>
        public string? ServiceDetails { get; set; }

        /// <summary>
        /// DTO名称
        /// </summary>
        public string DTOName { get; set; }

        /// <summary>
        /// 实体名称
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// 实体所在命名空间
        /// </summary>
        public string EntityNameSpace { get; set; }

        /// <summary>
        /// 实体所在完整DLL文件路径
        /// </summary>
        public string EntityDllPath { get; set; }

        /// <summary>
        /// 是否创建Mapper文件
        /// </summary>
        public bool IsBuildMapper { get; set; } = true;

        public void Check()
        {
            if (string.IsNullOrEmpty(BasePath))
                throw new Exception("【Generators】BasePath不能为空");
            if (string.IsNullOrEmpty(ControllerNameSpace))
                throw new Exception("【Generators】ControllerNameSpace不能为空");
            if (string.IsNullOrEmpty(ServiceNameSpace))
                throw new Exception("【Generators】ServiceNameSpace不能为空");
            if (string.IsNullOrEmpty(RepositoryNameSpace))
                throw new Exception("【Generators】RepositoryNameSpace不能为空");
            if (string.IsNullOrEmpty(DomainName))
                throw new Exception("【Generators】DomainName不能为空");
            if (string.IsNullOrEmpty(ServiceName))
                throw new Exception("【Generators】ServiceName不能为空");
            if (string.IsNullOrEmpty(RepositoryName))
                throw new Exception("【Generators】RepositoryName不能为空");
            if (string.IsNullOrEmpty(ServiceDetails))
                throw new Exception("【Generators】ServiceDetails不能为空");
            if (string.IsNullOrEmpty(DTOName))
                throw new Exception("【Generators】DTOName不能为空");
            if (string.IsNullOrEmpty(EntityName))
                throw new Exception("【Generators】EntityName不能为空");
            if (string.IsNullOrEmpty(EntityNameSpace))
                throw new Exception("【Generators】EntityNameSpace不能为空");
            if (string.IsNullOrEmpty(EntityDllPath))
                throw new Exception("【Generators】EntityDllPath不能为空");
        }
    }
}
