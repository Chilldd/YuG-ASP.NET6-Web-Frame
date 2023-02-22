using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using YuG.Framework.UnitTest;

namespace YuG.Framework.Tests
{
    public class Test : BaseTest<Program>
    {
        public Test(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public void yield()
        {
            var dto = new DTO();
            var res = GetResults(dto);
            foreach (var item in res)
            {
                _helper.WriteLine(item.Message);
            }
        }

        public IEnumerable<Result> GetResults(DTO data)
        {
            if (data.Name is null)
                yield return new Result() { Code = 500, Message = "名称不能为空" };
            if (data.Sex is null)
                yield return new Result() { Code = 500, Message = "性别不能为空" };
            if (data.Age is null)
                yield return new Result() { Code = 500, Message = "年龄不能为空" };
        }
    }

    public class DTO
    {
        public string? Name { get; set; }

        public int? Sex { get; set; }

        public int? Age { get; set; }
    }

    public class Result
    {
        public int Code { get; set; }

        public string? Message { get; set; }
    }
}
