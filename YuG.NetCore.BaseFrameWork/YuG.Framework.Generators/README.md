﻿

```c#
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
```

