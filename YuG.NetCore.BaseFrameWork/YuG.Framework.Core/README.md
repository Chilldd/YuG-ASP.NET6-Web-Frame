# YuG.Framework.Core





## 功能列表

- [x] IOC / AOP
- [x] Cors
- [x] Authorization
- [x] Log
- [x] 异常处理
- [x] Swagger
- [x] ORM
- [x] AutoMapper
- [x] 工具类
- [x] Redis

------

加载功能配置，需在appsettings添加YuGAppSettings节点，然后在添加对象功能节点

------



### IOC / AOP

- 引用外部包：

  - Autofac	6.3.0
  - Autofac.Extensions.DependencyInjection    7.1.0
  - Autofac.Extras.DynamicProxy    6.0.0

- Appsettings配置：

  - Ioc_AOP（object）：Ioc容器，AOP管理配置信息
    - QueryAllServiceUrl（string）：Ioc容器管理页面访问地址，页面可查看所有注入到Ioc中的类型。（默认值：/allservices）
    - MethodDataCache（bool）：是否开启方法缓存AOP。
    - MethodLog（bool）：是否开启方法日志AOP。
    - Tran（bool）：是否开启事务AOP。
    - Tenant（bool）：是否开启多租户AOP。
    - ServiceDLLName（string）：业务层名称。（不带后缀名）（case：YuG.Framework.Service）
    - RepositoryDLLName（string）：仓储层名称。（不带后缀名）（case：YuG.Framework.Repository）
    - IsServiceFromInterface（bool）：业务层是否继承接口
  - 对应配置类：
    - IocOption
    - 命名空间：YuG.Framework.Core.Ioc

- 使用方法：

  - 开启Ioc：

    - ```c#
      Program.cs
      
      builder.Host.UseServiceProviderFactory(new Autofac.Extensions.DependencyInjection.AutofacServiceProviderFactory());
      builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacModuleRegister()));
      ```

    - 注入规则：

      - 业务层：
        - 根据appsettings配置中IsServiceFromInterface字段判断是否需要通过接口注入
        - 只获取public类型的
        - 不会注入添加 [NotRegister] 特性的类
      - 仓储层：
        - 通过接口注入

  - 开启AOP：

    - ```
      开启AOP只需要在appsettings中添加对应节点信息
      
      AOP顺序：
      Log -> Cache -> Tenant -> Tran
      从左边开始，由外到内。如果全部切面都开启，那么Log先触发，Tran最后触发
      
      AOP只会对Service层的类进行切面处理，方法需要是public类型
      如果Service层没有继承接口，那么需要把Service层的方法加上virtual。如果继承接口则不需要。
      原因：
      https://autofac.readthedocs.io/en/latest/advanced/interceptors.html#enabling-interception
      原文：
      Class interception requires the methods being intercepted to be virtual since it uses subclassing as the proxy technique.
      ```

    - Log切面：

      - 不需要特性标注。

    - Tran切面：

      - 需要特性 [UseTran] 。
      - 需要开启ORM功能。
      - 开启事务功能后，方法内部为一个事务环境。方法正常完成则提交事务，方法抛出异常则事务回滚。

    - Tenant切面：

      - 需要特性 [Tenant] 。
      - 需要开启身份认证功能。
      - 需要在颁发Token时，设置用户的TenantID。
      - 开启多租户功能后，在执行方法前，切面会获取当前登录人对应的租户ID，然后切换至对应数据库连接。

    - Cache切面：

      - 未实现

        

## Cors

- 引用外部包：

  - Microsoft.AspNetCore.Cors	2.2.0

- Appsettings配置：

  - Cors（object）：Cors
    - IsAllowAnyMethod（bool）：是否开启所有方法都放行，如果为false，读取WhiteList中的地址。（默认值true）
    - WhiteList（string[]）：白名单列表。
  - 对应配置类：
    - CorsOption
    - 命名空间：YuG.Framework.Core.Cors

- 使用方法：

  - ```
    Program.cs
    
    var configuration = builder.Configuration;
    builder.Services.AddCorsSetup(configuration);
    
    app.UseCors(CorsSetup.CorsName);
    ```

  - 如果Program.cs中开启Cors，并且appsettings中没有配置，那么默认所有方法都放行。



## Authorization 

- 引用外部包：

  - Microsoft.AspNetCore.Authorization	6.0.0
  - Microsoft.AspNetCore.Authentication.JwtBearer 6.0.0

- Appsettings配置：

  - Authentication（object）：身份认证配置
    - ExpirationTime（int）：过期时间(默认30分钟)。
    - Audience（string）：订阅人。
    - Issuer（string）：发行人。
    - Secret（string）：秘钥。
    - CustomizeAuthorizationHandlerName（string）：自定义授权处理程序(完整的命名空间+类名)。
      - case："YuG.Framework.WebAPI.Extends.CustomizeAuthorizationHandler"
    - CustomizeAuthorizationHandlerDLLName（string）：自定义授权处理程序所在的DLL文件名称 。
      - case： "YuG.Framework.WebAPI.dll"
  - 对应配置类：
    - AuthenticationOption
    - 命名空间：YuG.Framework.Authorization

- 使用方法：

  - ```
    Program.cs
    
    var configuration = builder.Configuration;
    builder.Services.AddAuthenticationSetup(configuration);
    
    app.UseAuthenticationMiddleware(configuration);
    ```

  - 需要开启授权的方法或控制器添加：[Authorize] 特性

  - 认证：

    - 默认走Jwt认证

  - 授权：

    - 默认授权不做任何处理
    - 自定义授权：
      - appsettings中配置： CustomizeAuthorizationHandlerName 和 CustomizeAuthorizationHandlerDLLName 节点
      - 自定义授权处理程序需要继承  AuthorizationHandler<IAuthorizationRequirement>
      - 实现 HandleRequirementAsync 方法

  - 帮助类：

    - ITokenHelper：
      - 实现类：JwtHelper
      - Token帮助类
      - 颁发Token，解析Token
      - 已通过Ioc注入，直接构造函数获取即可
    - IUserInfoHelper
      - 实现类：CurrentUserInfoHelper
      - 当前登录用户帮助类
      - 获取请求中的Token，用户信息等
      - 已通过Ioc注入，直接构造函数获取即可