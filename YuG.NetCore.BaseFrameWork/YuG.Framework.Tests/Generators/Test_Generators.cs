using YuG.Framework.Generators;
using YuG.Framework.UnitTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace YuG.Framework.Tests.Generators
{
    public class Test_Generators : BaseTest<Program>
    {
        public Test_Generators(ITestOutputHelper helper) : base(helper)
        {
        }

        /// <summary>
        /// 生成实体
        /// </summary>
        [Fact]
        public void BuildEntidty()
        {
            var dbOption = new DbOption("Data Source=D:\\Work\\File\\SqliteDb\\fisk_framework_dev.db", SqlSugar.DbType.Sqlite);
            var option = new BuildEntityOption("YuG.Framework.Model", @"D:\Work\Code\NETWorkSpace\YuG.NetCore.BaseFrameWork\YuG.Framework.Model\", dbOption);
            //EntityInfos为空时，生成所有表的实体
            option.EntityInfos = new List<EntityInfo>();
            option.IsExtend = true;
            DbFirstHelper.BuildEntity(option);
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        [Fact]
        public void BuildServiceCode()
        {
            var dbOption = new DbOption("Data Source=D:\\Work\\File\\SqliteDb\\fisk_framework_dev.db", SqlSugar.DbType.Sqlite);
            var option = new BuildServiceCodeOption(dbOption);
            option.BasePath = @"D:\Work\Code\NETWorkSpace\YuG.NetCore.BaseFrameWork";
            option.ControllerNameSpace = "YuG.Framework.WebAPI";
            option.ServiceNameSpace = "YuG.Framework.Service";
            option.RepositoryNameSpace = "YuG.Framework.Repository";
            option.DomainName = "SystemManage";
            option.DTOName = "RoleInfo";
            option.EntityName = "TbRoleEntity";
            option.EntityNameSpace = "YuG.Framework.Model";
            option.EntityDllPath = @"D:\Work\Code\NETWorkSpace\YuG.NetCore.BaseFrameWork\YuG.Framework.Model\bin\Debug\net6.0\YuG.Framework.Model.dll";
            option.ServiceName = "RoleInfo";
            option.ServiceDetails = "角色管理";
            option.RepositoryName = "RoleInfo";
            option.IsBuildMapper = true;
            DbFirstHelper.BuildServiceCode(option);
        }

        [Fact]
        public void test()
        {
            string str = "System.Nullable<int>";
            Regex re = new Regex(@"(?<=\<)[a-zA-Z_]+(?=\>)");
            _helper.WriteLine(re.Match(str).Value);
        }
    }
}
