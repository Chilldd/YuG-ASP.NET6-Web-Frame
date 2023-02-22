using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using YuG.Framework.UnitTest;

namespace YuG.Framework.Tests.Authorization.Test_Authorization
{
    public class Test_Authorization : BaseTest<Program>
    {
        public Test_Authorization(ITestOutputHelper helper) : base(helper)
        {

        }

        /// <summary>
        /// 颁发Token
        /// </summary>
        [Fact]
        public void Test_IssueJwt()
        {
            //Arrange
            var content = base.buildHttpRequestMessage(HttpMethod.Get, "api/APITest/GetCurrentUserInfo", true);
            var client = _application.CreateClient();


        }
    }
}
