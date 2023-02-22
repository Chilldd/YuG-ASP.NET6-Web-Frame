using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Core.Swagger
{
    /// <summary>
    /// Swagger中间件
    /// </summary>
    public static class SwaggerMiddleware
    {
        public static void UseSwaggerMilddleware(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            var option = (app.ApplicationServices.GetService(typeof(IOptions<SwaggerOption>)) as IOptions<SwaggerOption>).Value;

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{option.Version}/swagger.json", $"{option.Title} {option.Version}");

                //Swagger访问前缀
                if (!string.IsNullOrEmpty(option.RoutePrefix))
                    c.RoutePrefix = option.RoutePrefix;
            });

            SystemServerInformationHelper.OpenMiddlewareInformation(SystemServerEnum.Swagger, $"访问地址：【/{option.RoutePrefix}/index.html】");
        }
    }
}
