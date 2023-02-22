using System;
using System.Linq;
using System.Text;
using YuG.Framework;
using SqlSugar;


namespace YuG.Framework.Model
{
    
    /// <summary>
    /// TableName: tb_user_role
    /// TableDescription: 
    /// </summary>
    [SqlSugar.SugarTable("tb_user_role")]
    public partial class TbUserRoleEntity : BaseEntity
    {
        public TbUserRoleEntity()
        {
            
        }


        /// <summary>
        /// Desc: 
        /// Default: 
        /// Nullable: False
        /// </summary>
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
        public int ID { get; set; }


        /// <summary>
        /// Desc: 
        /// Default: 
        /// Nullable: False
        /// </summary>
           [SugarColumn(ColumnName="user_id")]
        public int UserID { get; set; }


        /// <summary>
        /// Desc: 
        /// Default: 
        /// Nullable: False
        /// </summary>
           [SugarColumn(ColumnName="role_id")]
        public int RoleID { get; set; }

    }
}
