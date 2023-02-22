using System;
using System.Linq;
using System.Text;
using YuG.Framework;
using SqlSugar;


namespace YuG.Framework.Model
{
    
    /// <summary>
    /// TableName: tb_role
    /// TableDescription: 
    /// </summary>
    [SqlSugar.SugarTable("tb_role")]
    public partial class TbRoleEntity : BaseEntity
    {
        public TbRoleEntity()
        {
            
        }


        /// <summary>
        /// Desc: 
        /// Default: 
        /// Nullable: False
        /// </summary>
           [SugarColumn(ColumnName="name")]
        public string Name { get; set; }

    }
}
