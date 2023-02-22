using System;
using System.Linq;
using System.Text;
using YuG.Framework;
using SqlSugar;


namespace YuG.Framework.Model
{
    
    /// <summary>
    /// TableName: tb_test
    /// TableDescription: 
    /// </summary>
    [SqlSugar.SugarTable("tb_test")]
    public partial class TbTestEntity : BaseEntity
    {
        public TbTestEntity()
        {
            
        }


        /// <summary>
        /// Desc: 
        /// Default: 
        /// Nullable: True
        /// </summary>
           [SugarColumn(ColumnName="name")]
        public string Name { get; set; }


        /// <summary>
        /// Desc: 
        /// Default: 
        /// Nullable: True
        /// </summary>
           [SugarColumn(ColumnName="value")]
        public string Value { get; set; }

    }
}
