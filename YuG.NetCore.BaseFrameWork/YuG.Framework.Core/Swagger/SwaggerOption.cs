using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Core.Swagger
{
    /// <summary>
    /// Swagger配置信息
    /// </summary>
    public class SwaggerOption : IModelCheck
    {
        public static string SwaggerOptionName = "YuGAppSettings:Swagger";

        public string ProjectName { get; set; } = "YuG.Framework";
        public string Version { get; set; } = "v1";
        public string Title { get; set; } = ".NET6 WEB API";
        public string Description { get; set; } = "YuG .NET6 Framework";
        public string[] ProjectDetails { get; set; }
        public string RoutePrefix { get; set; }

        public Contact Contact { get; set; } = new Contact();

        public License License { get; set; } = new License();

        public void CheckModel()
        {

        }
    }

    public class Contact
    {
        public string Name { get; set; } = "GY";
        public string Email { get; set; } = "guoyu@fisksoft.com";
        public string Url { get; set; }
    }

    public class License
    {
        public string Name { get; set; } = "GY";
        public string Url { get; set; }
    }
}

