using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Core.Swagger
{
    /// <summary>
    /// Swagger服务
    /// </summary>
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            string serverName = SystemServerEnum.Swagger.GetEnumDescription();

            //get option
            var section = configuration.GetSection(SwaggerOption.SwaggerOptionName);
            var option = section.Get<SwaggerOption>();
            if (option is null)
                option = new SwaggerOption();
            services.Configure<SwaggerOption>(section);

            var basePath = AppContext.BaseDirectory;

            //add swagger
            services.AddSwaggerGen(c =>
            {
                //描述信息
                c.SwaggerDoc(option.Version, new OpenApiInfo
                {
                    Version = option.Version,
                    Title = option.Title,
                    Description = option.Description,
                    Contact = new OpenApiContact
                    {
                        Name = option.Contact.Name,
                        Email = option.Contact.Email,
                        Url = string.IsNullOrWhiteSpace(option.Contact.Url) ? null : new Uri(option.Contact.Url)
                    },
                    License = new OpenApiLicense
                    {
                        Name = option.License.Name,
                        Url = string.IsNullOrWhiteSpace(option.License.Url) ? null : new Uri(option.License.Url)
                    }
                });
                //排序规则
                c.OrderActionsBy(o => o.RelativePath);

                //加载项目描述文件
                try
                {
                    foreach (var item in option.ProjectDetails)
                    {
                        var xmlPath = Path.Combine(basePath, item);
                        c.IncludeXmlComments(xmlPath);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"【{serverName}】缺少项目xml文件");
                }

                //开启身份认证
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                //在header中添加token
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                //Jwt Bearer认证，必须是oauth2
                c.AddSecurityDefinition(AuthenticationConstant.SecurityDefinition,
                                        new OpenApiSecurityScheme
                                        {
                                            Description = "请输入Token。格式：Bearer token",
                                            Name = AuthenticationConstant.AuthorizationHeaderName,
                                            In = ParameterLocation.Header,
                                            Type = SecuritySchemeType.ApiKey
                                        });
            });

            SystemServerInformationHelper.OpenServerInformation(SystemServerEnum.Swagger);
        }
    }
}
