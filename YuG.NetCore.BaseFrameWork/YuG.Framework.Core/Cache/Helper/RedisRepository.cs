using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Core.Cache
{
    public class RedisRepository : IRedisRepository
    {
        private readonly ConnectionMultiplexer _connection;

        public RedisRepository(ConnectionMultiplexer connection)
        {
            this._connection = connection;
        }

        private IDatabase GetDb(int db)
        {
            return _connection.GetDatabase(db);
        }

        /// <summary>
        /// 根据key获取缓存
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<TResult> GetAsync<TResult>(string key, int db = -1)
        {
            var res = await GetDb(db).StringGetAsync(key);
            if (res.HasValue)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<TResult>(res);
            }
            return default(TResult);
        }

        /// <summary>
        /// 根据key获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string> GetAsync(string key, int db = -1)
        {
            return await GetDb(db).StringGetAsync(key);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public async Task SetAsync<T>(string key, T value, TimeSpan time, int db = -1)
        {
            if (value != null)
            {
                if (value is string cacheValue)
                {
                    await GetDb(db).StringSetAsync(key, cacheValue, time);
                }
                else
                {
                    await GetDb(db).StringSetAsync(key, Newtonsoft.Json.JsonConvert.SerializeObject(value), time);
                }
            }
        }

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> ExistAsync(string key, int db = -1)
        {
            return await GetDb(db).KeyExistsAsync(key);
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task RemoveAsync(string key, int db = -1)
        {
            await GetDb(db).KeyDeleteAsync(key);
        }

        public async Task<string> HGetAsync(string key, string hashKey, int db = -1)
        {
            return await GetDb(db).HashGetAsync(key, hashKey);
        }

        public async Task<TResult> HGetAsync<TResult>(string key, string hashKey, int db = -1)
        {
            var res = await GetDb(db).HashGetAsync(key, hashKey);
            if (res.HasValue)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<TResult>(res);
            }
            return default(TResult);
        }

        public async Task<HashEntry[]> HGetAllAsync(string key, int db = -1)
        {
            return await GetDb(db).HashGetAllAsync(key);
        }

        public async Task<bool> HExistsAsync(string key, string hashKey, int db = -1)
        {
            return await GetDb(db).HashExistsAsync(key, hashKey);
        }

        public async Task<RedisValue[]> HGetKeysAsync(string key, int db = -1)
        {
            return await GetDb(db).HashKeysAsync(key);
        }

        public async Task<RedisValue[]> HGetValuesAsync(string key, int db = -1)
        {
            return await GetDb(db).HashValuesAsync(key);
        }

        public async Task<bool> HSetAsync(string key, string hashKey, string itemValue, When when = When.Always, int db = -1)
        {
            return await GetDb(db).HashSetAsync(key, hashKey, itemValue, when);
        }

        public async Task<bool> HSetAsync<TParams>(string key, string hashKey, TParams itemValue, When when = When.Always, int db = -1)
        {
            return await GetDb(db).HashSetAsync(key, hashKey, Newtonsoft.Json.JsonConvert.SerializeObject(itemValue), when);
        }

        public async Task<bool> HRemoveAsync(string key, string hashKey, int db = -1)
        {
            return await GetDb(db).HashDeleteAsync(key, hashKey);
        }

        public async Task<long> IncrementAsync(string key, long value = 1L, int db = -1)
        {
            return await GetDb(db).StringIncrementAsync(key, value);
        }

        public async Task<long> DecrementAsync(string key, long value = 1L, int db = -1)
        {
            return await GetDb(db).StringDecrementAsync(key, value);
        }

        public async Task<long> ListLeftPushAsync(string redisKey, string redisValue, int db = -1)
        {
            return await GetDb(db).ListLeftPushAsync(redisKey, redisValue);
        }

        public async Task<long> ListRightPushAsync(string redisKey, string redisValue, int db = -1)
        {
            return await GetDb(db).ListRightPushAsync(redisKey, redisValue);
        }

        public async Task<long> ListRightPushAsync(string redisKey, IEnumerable<string> redisValue, int db = -1)
        {
            var redislist = new List<RedisValue>();
            foreach (var item in redisValue)
            {
                redislist.Add(item);
            }
            return await GetDb(db).ListRightPushAsync(redisKey, redislist.ToArray());
        }

        public async Task<T> ListLeftPopAsync<T>(string redisKey, int db = -1) where T : class
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(await GetDb(db).ListLeftPopAsync(redisKey));
        }

        public async Task<T> ListRightPopAsync<T>(string redisKey, int db = -1) where T : class
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(await GetDb(db).ListRightPopAsync(redisKey));
        }

        public async Task<string> ListLeftPopAsync(string redisKey, int db = -1)
        {
            return await GetDb(db).ListLeftPopAsync(redisKey);
        }

        public async Task<string> ListRightPopAsync(string redisKey, int db = -1)
        {
            return await GetDb(db).ListRightPopAsync(redisKey);
        }

        public async Task<long> ListLengthAsync(string redisKey, int db = -1)
        {
            return await GetDb(db).ListLengthAsync(redisKey);
        }

        public async Task<IEnumerable<string>> ListRangeAsync(string redisKey, int db = -1)
        {
            var result = await GetDb(db).ListRangeAsync(redisKey);
            return result.Select(o => o.ToString());
        }

        public async Task<IEnumerable<string>> ListRangeAsync(string redisKey, int start, int stop, int db = -1)
        {
            var result = await GetDb(db).ListRangeAsync(redisKey, start, stop);
            return result.Select(o => o.ToString());
        }

        public async Task<long> ListDelRangeAsync(string redisKey, string redisValue, long type = 0, int db = -1)
        {
            return await GetDb(db).ListRemoveAsync(redisKey, redisValue, type);
        }

        public async Task ListClearAsync(string redisKey, int db = -1)
        {
            await GetDb(db).ListTrimAsync(redisKey, 1, 0);
        }
    }
}
