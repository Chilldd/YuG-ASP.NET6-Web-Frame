using YuG.Framework.Core.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using YuG.Framework.UnitTest;
using Xunit.Abstractions;

namespace YuG.Framework.Tests.Cache
{
    public class Test_Redis : BaseTest<Program>
    {
        private readonly IRedisRepository _redis;
        public Test_Redis(ITestOutputHelper helper) : base(helper)
        {
            _redis = (_application.Services.GetService(typeof(IRedisRepository)) as IRedisRepository)!;
        }

        [Fact]
        public async Task Test_Redis_Hash_Get()
        {
            string key = "Test_Hash",
                   hashKey = "1002",
                   hashValue = "abc3";
            bool res = await _redis.HSetAsync(key, hashKey, hashValue);
            _helper.WriteLine(res.ToString());

            string value = await _redis.HGetAsync(key, hashKey);
            _helper.WriteLine(value);

            bool removeRes = await _redis.HRemoveAsync(key, hashKey);
            _helper.WriteLine(removeRes.ToString());
            Assert.True(removeRes);
        }

        [Fact]
        public async Task Test_Redis_Hash_Get2()
        {
            string key = "Test_Hash",
                   hashKey = "1002";
            var hashValue = new Student();
            bool res = await _redis.HSetAsync(key, hashKey, hashValue);
            _helper.WriteLine(res.ToString());

            Student value = await _redis.HGetAsync<Student>(key, hashKey);
            _helper.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(value));

            bool removeRes = await _redis.HRemoveAsync(key, hashKey);
            _helper.WriteLine(removeRes.ToString());
            Assert.True(removeRes);
        }

        [Fact]
        public async Task Test_Redis_Hash_GetAll()
        {
            string key = "Test_Hash";
            var res = await _redis.HGetAllAsync(key);
            foreach (var item in res)
            {
                _helper.WriteLine($"Key: {item.Name},Value: {item.Value}");
            }
        }

        [Fact]
        public async Task Test_Redis_Hash_Exists()
        {
            string key = "Test_Hash",
                   hashKey = "1003";
            Assert.True(await _redis.HExistsAsync(key, hashKey));
        }

        [Fact]
        public async Task Test_Redis_Hash_GetKeys()
        {
            string key = "Test_Hash";
            var res = await _redis.HGetKeysAsync(key);
            foreach (var item in res)
            {
                _helper.WriteLine(item);
            }
        }

        [Fact]
        public async Task Test_Redis_Hash_GetValues()
        {
            string key = "Test_Hash";
            var res = await _redis.HGetValuesAsync(key);
            foreach (var item in res)
            {
                _helper.WriteLine(item);
            }
        }

        [Fact]
        public async Task Test_Redis_Increment()
        {
            string key = "Test_Increment";
            long value = 1000L;
            var res = await _redis.IncrementAsync(key, value);
            _helper.WriteLine($"Value: {value}, Res: {res}");
        }

        [Fact]
        public async Task Test_Redis_Decrement()
        {
            string key = "Test_Decrement";
            long value = 100L;
            var res = await _redis.DecrementAsync(key, value);
            _helper.WriteLine($"Value: {value}, Res: {res}");
        }

        [Fact]
        public async Task Test_Redis_List_Push()
        {
            string key = "Test_List";
            await _redis.ListLeftPushAsync(key, "4");
            await _redis.ListRightPushAsync(key, "-1");
        }

        [Fact]
        public async Task Test_Redis_List_Get()
        {
            string key = "Test_List";
            string value = await _redis.ListLeftPopAsync(key);
            string value2 = await _redis.ListRightPopAsync(key);
            _helper.WriteLine(value);
            _helper.WriteLine(value2);
        }

        [Fact]
        public async Task Test_Redis_List_Range()
        {
            string key = "Test_List";
            var value = await _redis.ListRangeAsync(key);
            foreach (var item in value)
            {
                _helper.WriteLine(item.ToString());
            }

            _helper.WriteLine("********************");

            var value2 = await _redis.ListRangeAsync(key, 0, 2);
            foreach (var item in value2)
            {
                _helper.WriteLine(item.ToString());
            }
        }

        [Fact]
        public async Task Test_Redis_List_DelRange()
        {
            string key = "Test_List";
            var value = await _redis.ListDelRangeAsync(key, "2", -1);
        }

        [Fact]
        public async Task Test_Redis_List_Length()
        {
            string key = "Test_List";
            var value = await _redis.ListLengthAsync(key);
            _helper.WriteLine(value.ToString());
        }

        [Fact]
        public async Task Test_Redis_List_Clear()
        {
            string key = "Test_List";
            await _redis.ListClearAsync(key);
        }
    }

    public class Student
    {
        public int Id { get; set; } = 1;
        public string Name { get; set; } = "Tom";
        public int Age { get; set; } = 18;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
