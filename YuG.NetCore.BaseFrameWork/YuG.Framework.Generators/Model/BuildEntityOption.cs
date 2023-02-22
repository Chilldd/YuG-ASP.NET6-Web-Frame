using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Generators
{
    public class BuildEntityOption : BaseOption
    {
        public BuildEntityOption(string nameSpace, string path, DbOption dbOption) : base(dbOption)
        {
            NameSpace = nameSpace;
            Path = path;
        }

        /// <summary>
        /// 需要生成的表信息
        /// 如果为null，生成所有表
        /// </summary>
        public List<EntityInfo>? EntityInfos { get; set; }

        /// <summary>
        /// 实体所属命名空间
        /// </summary>
        public string NameSpace { get; set; }

        /// <summary>
        /// Path(生成的实体类存放目录)
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 是否继承BaseEntity
        /// </summary>
        public bool IsExtend { get; set; } = true;
    }

    public class EntityInfo
    {
        public EntityInfo(string tableName, string? entityName)
        {
            TableName = tableName;
            EntityName = entityName;
        }

        public string TableName { get; set; }
        public string? EntityName { get; set; }
    }
}
