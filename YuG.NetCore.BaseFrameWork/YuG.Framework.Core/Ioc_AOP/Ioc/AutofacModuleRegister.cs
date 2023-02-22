using Autofac;
using Autofac.Extras.DynamicProxy;
using YuG.Framework.Core.AOP;
using YuG.Framework.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Module = Autofac.Module;

namespace YuG.Framework.Core.Ioc.Autofac
{
    public class AutofacModuleRegister : Module
    {

        //重写Autofac管道Load方法，在这里注册注入
        protected override void Load(ContainerBuilder builder)
        {
            string serverName = SystemServerEnum.IocAOP.GetEnumDescription();
            //load option
            var config = AppsettingsHelper.GetConfiguration();
            var option = config.GetSection(IocOption.ApplicationOptionName).Get<IocOption>();
            if (option is null)
                throw new YuGCustomizeException($"【{serverName}】缺少配置信息【{IocOption.ApplicationOptionName}】");
            option.CheckModel();

            var basePath = AppContext.BaseDirectory;

            var servicesDllFile = Path.Combine(basePath, $"{option.ServiceDLLName}.dll");
            var repositoryDllFile = Path.Combine(basePath, $"{option.RepositoryDLLName}.dll");

            if (!File.Exists(servicesDllFile))
                throw new ArgumentNullException($"【{serverName}】文件未找到【{option.ServiceDLLName}】");
            if (!File.Exists(repositoryDllFile))
                throw new ArgumentNullException($"【{serverName}】文件未找到【{option.RepositoryDLLName}】");

            Assembly service = Assembly.LoadFrom(servicesDllFile);
            Assembly repository = Assembly.LoadFrom(repositoryDllFile);


            #region AOP

            /*
             * 注册切面
             * 切面可以多层，类似中间件
             */
            var aopList = new List<Type>(3);
            if (option.MethodLog)
            {
                builder.RegisterType<MethodLogAspect>();
                aopList.Add(typeof(MethodLogAspect));
            }
            if (option.MethodDataCache)
            {
                builder.RegisterType<MethodDataCacheAspect>();
                aopList.Add(typeof(MethodDataCacheAspect));
            }
            if (option.Tenant)
            {
                builder.RegisterType<TenantAspect>();
                aopList.Add(typeof(TenantAspect));
            }
            if (option.Tran)
            {
                builder.RegisterType<TranAspect>();
                aopList.Add(typeof(TranAspect));
            }

            #endregion AOP

            #region 程序集批量注册

            /** 备注
             * .As()：需要指定实现是哪个接口
             * .AsImplementedInterfaces()：自动实现所有接口
             * case：类A，实现了两个接口（B,C）
             *       .As<B>()   =》 As说明了实现的是B接口，那么只会暴露出B类型
             *       .As(e => e.GetInterfaces()[0]) =》GetInterfaces()[0]获取的是第一个实现的接口，所以也只能暴露出B类型
             *       .AsImplementedInterfaces() =》自动将实现的所有接口全部暴露出去，B,C都能暴露
             *       
             * EnableInterfaceInterceptors和EnableClassInterceptors区别：
             * EnableInterfaceInterceptors：需要注入的类实现接口
             * EnableClassInterceptors：不实现接口，通过虚方法实现 
             * https://autofaccn.readthedocs.io/zh/latest/advanced/interceptors.html#enabling-interception
             */

            #region 注册服务层
            if (option.IsServiceFromInterface)
            {
                builder.RegisterAssemblyTypes(service)  //获取命名空间
                       .PublicOnly()   //只要public访问权限的
                       .Where(e =>
                       {
                           var attr = e.GetCustomAttribute<NotRegisterAttribute>();
                           return attr is null && e.IsClass;
                       })
                       .AsImplementedInterfaces()
                       .InstancePerDependency()
                       .EnableInterfaceInterceptors()  //开启拦截, 需要注入的类实现接口
                       .InterceptedBy(aopList.ToArray()); //注册多个切面
            }
            else
            {

                builder.RegisterAssemblyTypes(service)  //获取命名空间
                       .PublicOnly()   //只要public访问权限的
                       .Where(e =>
                       {
                           var attr = e.GetCustomAttribute<NotRegisterAttribute>();
                           return attr is null && e.IsClass;
                       })
                       .InstancePerDependency()
                       .EnableClassInterceptors()   //开启拦截, 只会拦截虚方法，重写方法
                       .InterceptedBy(aopList.ToArray()); //注册多个切面
            }

            #endregion 注册服务层

            #region 注册仓储层

            builder.RegisterAssemblyTypes(repository)  //获取命名空间
                   .PublicOnly()   //只要public访问权限的
                   .AsImplementedInterfaces()  //自动以其实现的所有接口类型暴露（包括IDisposable接口）
                   .InstancePerDependency();

            #endregion 注册仓储层

            #endregion 程序集批量注册

            SystemServerInformationHelper.OpenServerInformation(SystemServerEnum.IocAOP, $"已开启切面：【{string.Join(',', aopList.Select(e => e.Name))}】");
        }
    }
}
