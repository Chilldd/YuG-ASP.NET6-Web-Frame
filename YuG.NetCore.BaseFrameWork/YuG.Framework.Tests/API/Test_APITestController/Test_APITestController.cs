using YuG.Framework.UnitTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace YuG.Framework.Tests.API.Test_APITestController
{
    public class Test_APITestController : BaseTest<Program>
    {
        public Test_APITestController(ITestOutputHelper helper) : base(helper)
        {
        }

        /// <summary>
        /// 获取当前登录人信息
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Test_APITest_GetCurrentUserInfo()
        {
            //Arrange
            var content = base.buildHttpRequestMessage(HttpMethod.Get, "api/APITest/GetCurrentUserInfo", true);
            var client = _application.CreateClient();

            //Actd
            var response = await client.SendAsync(content);
            var res = await response.Content.ReadAsStringAsync();

            //Assert
            _helper.WriteLine(res);
            Assert.True(response is not null && response.StatusCode == HttpStatusCode.OK);
            Assert.NotEmpty(res);
        }

        /// <summary>
        /// 获取当前请求中的Token信息
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Test_APITest_GetCurrentToken()
        {
            //Arrange
            var content = base.buildHttpRequestMessage(HttpMethod.Get, "api/APITest/GetCurrentToken", true);
            var client = _application.CreateClient();

            //Actd
            var response = await client.SendAsync(content);
            var res = await response.Content.ReadAsStringAsync();

            //Assert
            _helper.WriteLine(res);
            Assert.True(response is not null && response.StatusCode == HttpStatusCode.OK);
            Assert.NotEmpty(res);
        }
    }
}
