using YuG.Framework.Authorization;
using YuG.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace YuG.Framework.UnitTest
{
    public class BaseTest<T> where T : class
    {
        private TokenModel _tokenModel = new TokenModel() { ID = 1, Account = "admin", Name = "admin", TenantID = 0 };

        protected readonly ITestOutputHelper _helper;
        protected readonly WebApplicationFactory<T> _application;

        public BaseTest(ITestOutputHelper helper)
        {
            this._helper = helper;
            this._application = new WebApplicationFactory<T>().WithWebHostBuilder(builder => { });
        }

        protected HttpRequestMessage buildHttpRequestMessage(HttpMethod httpMethod, string url, bool isAuth = false, TokenModel? tokenModel = null)
        {
            var tokenHelper = _application.Services.GetService(typeof(ITokenHelper)) as ITokenHelper;
            var http = new HttpRequestMessage(httpMethod, url);
            if (isAuth)
            {
                var tokenName = AuthenticationConstant.AuthenticationSchemeStr + tokenHelper?.IssueJwt(tokenModel ?? _tokenModel);
                http.Headers.Add(AuthenticationConstant.AuthorizationHeaderName, tokenName);
            }
            return http;
        }
    }
}
