using YuG.Framework.Authorization;
using YuG.Framework.UnitTest;
using YuG.Framework.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace YuG.Framework.Tests.Authorization.Test_Token
{
    public class Test_Token : BaseTest<Program>
    {
        /// <summary>
        /// 测试Model
        /// </summary>
        private TokenModel _tokenModel = new TokenModel
        {
            ID = 1,
            Account = "admin",
            Name = "admin",
            TenantID = 1
        };
        private ITokenHelper? tokenHelper;

        public Test_Token(ITestOutputHelper helper) : base(helper)
        {
            tokenHelper = _application.Services.GetService(typeof(ITokenHelper)) as ITokenHelper;
        }

        /// <summary>
        /// 颁发Token
        /// </summary>
        [Fact]
        public void Test_IssueJwt()
        {
            //Act
            var token = tokenHelper?.IssueJwt(_tokenModel);

            //Assert
            _helper.WriteLine(token);
            Assert.NotEmpty(token);
        }

        /// <summary>
        /// 解析Token
        /// </summary>
        /// <param name="token"></param>
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("test")]
        [InlineData("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIxIiwiQWNjb3VudCI6ImFkbWluIiwiTmFtZSI6ImFkbWluIiwiVGVuYW50SUQiOiIxIiwiaWF0IjoiMTYzNzU2NDYzMiIsIm5iZiI6IjE2Mzc1NjQ2MzIiLCJleHAiOiIxNjQwMTU2NjMyIiwiaXNzIjoiRmlzay5ORVQ2LlByb2plY3QiLCJhdWQiOiJneSJ9.Es6c8PVZLTFXXxqW3Xqa1U_W94h1RsMlDkkiQesEfm8")]
        [InlineData("Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIxIiwiQWNjb3VudCI6ImFkbWluIiwiTmFtZSI6ImFkbWluIiwiVGVuYW50SUQiOiIxIiwiaWF0IjoiMTYzNzU2NDYzMiIsIm5iZiI6IjE2Mzc1NjQ2MzIiLCJleHAiOiIxNjQwMTU2NjMyIiwiaXNzIjoiRmlzay5ORVQ2LlByb2plY3QiLCJhdWQiOiJneSJ9.Es6c8PVZLTFXXxqW3Xqa1U_W94h1RsMlDkkiQesEfm8")]
        public void Test_SerializeJwt(string token)
        {
            //Act
            var tokenModel = tokenHelper?.SerializeJwt(token);

            //Assert
            _helper.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(tokenModel));
            Assert.Equal(_tokenModel.ID, tokenModel?.ID);
        }

        /// <summary>
        /// 颁发Token并解析
        /// </summary>
        [Fact]
        public void Test_IssueJwtAndSerializeJwt()
        {
            //Act
            var token = tokenHelper?.IssueJwt(_tokenModel);

            //Assert
            _helper.WriteLine(token);
            Assert.NotEmpty(token);

            //Act
            var tokenModel = tokenHelper?.SerializeJwt(token);

            //Assert
            _helper.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(tokenModel));
            Assert.Equal(_tokenModel.ID, tokenModel?.ID);
        }
    }
}
