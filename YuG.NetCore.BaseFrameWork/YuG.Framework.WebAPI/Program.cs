global using YuG.Framework.Core;

using Autofac;
using YuG.Framework;
using YuG.Framework.Authorization;
using YuG.Framework.Core.AutoMapper;
using YuG.Framework.Core.Cache;
using YuG.Framework.Core.Cors;
using YuG.Framework.Core.ExceptionHandler;
using YuG.Framework.Core.Ioc;
using YuG.Framework.Core.Ioc.Autofac;
using YuG.Framework.Core.Swagger;
using YuG.Framework.ORM.Sqlsugar;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

#region Add services to the container.
//Register System Service
builder.Services.AddBaseRegisterSetup(configuration);
//Autofac Ioc Manage
builder.Host.UseServiceProviderFactory(new Autofac.Extensions.DependencyInjection.AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacModuleRegister()));
//Log
builder.Logging.Configure(options =>
                          {
                              options.ActivityTrackingOptions = ActivityTrackingOptions.SpanId
                                                                  | ActivityTrackingOptions.TraceId
                                                                  | ActivityTrackingOptions.ParentId
                                                                  | ActivityTrackingOptions.Baggage
                                                                  | ActivityTrackingOptions.Tags;
                          })
               .AddLog4Net("Log4net.config");
//Add Cors
builder.Services.AddCorsSetup(configuration);
//Add Controller & Add Controller Global Exception
builder.Services.AddControllers(e => ExceptionHandlerSetup.Interceptor(e, configuration))
                .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
//Add Model Verify Custom Return Result
builder.Services.AddModelVerifySetup(configuration);
//Swagger
builder.Services.AddEndpointsApiExplorer();
//Add Authentication
builder.Services.AddAuthenticationSetup(configuration);
builder.Services.AddSwaggerSetup(configuration);
//Sqlsugar SqlLite
builder.Services.AddSqlsugarSetup(configuration);
//Add AutoMapper
builder.Services.AddAutoMapperSetup();
//Add Cache
builder.Services.AddCacheSetup(configuration);
#endregion

var app = builder.Build();

#region Configure the HTTP request pipeline.
//Swagger
app.UseSwaggerMilddleware();
//Cors
app.UseCors(CorsSetup.CorsName);
//Query All Register Service
app.UseAllServiceMilddleware(builder.Services, configuration);
//Authentication
app.UseAuthenticationMiddleware(configuration);

app.MapControllers();

app.Run();
#endregion


/// <summary>
/// 扩展类(单元测试)
/// </summary>
public partial class Program { }