using System;
using System.Linq;
using System.Text;
using YuG.Framework;
using SqlSugar;


namespace YuG.Framework.Model
{
    
    /// <summary>
    /// TableName: tb_user
    /// TableDescription: 
    /// </summary>
    [SqlSugar.SugarTable("tb_user")]
    public partial class TbUserEntity : BaseEntity
    {
        public TbUserEntity()
        {
            
        }


        /// <summary>
        /// Desc: 
        /// Default: 
        /// Nullable: False
        /// </summary>
           [SugarColumn(ColumnName="account")]
        public string Account { get; set; }


        /// <summary>
        /// Desc: 
        /// Default: 
        /// Nullable: False
        /// </summary>
           [SugarColumn(ColumnName="password")]
        public string Password { get; set; }


        /// <summary>
        /// Desc: 
        /// Default: 
        /// Nullable: False
        /// </summary>
           [SugarColumn(ColumnName="name")]
        public string Name { get; set; }


        /// <summary>
        /// Desc: 
        /// Default: 
        /// Nullable: True
        /// </summary>
           [SugarColumn(ColumnName="email")]
        public string Email { get; set; }


        /// <summary>
        /// Desc: 
        /// Default: 
        /// Nullable: True
        /// </summary>
           [SugarColumn(ColumnName="phone")]
        public string Phone { get; set; }


        /// <summary>
        /// Desc: 
        /// Default: 
        /// Nullable: True
        /// </summary>
           [SugarColumn(ColumnName="status")]
        public int? Status { get; set; }

    }
}
