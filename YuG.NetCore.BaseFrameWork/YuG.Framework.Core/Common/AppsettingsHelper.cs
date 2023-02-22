using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Core
{
    public class AppsettingsHelper
    {
        private static IConfiguration Configuration { get; set; }
        public AppsettingsHelper(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration GetConfiguration() => Configuration;
    }
}
