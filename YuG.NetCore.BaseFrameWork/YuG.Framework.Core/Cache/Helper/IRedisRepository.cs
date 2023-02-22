using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.Core.Cache
{
    public interface IRedisRepository
    {

        /// <summary>
        /// 根据key获取缓存
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<TResult> GetAsync<TResult>(string key, int db = -1);

        /// <summary>
        /// 根据key获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> GetAsync(string key, int db = -1);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        Task SetAsync<T>(string key, T value, TimeSpan time, int db = -1);

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> ExistAsync(string key, int db = -1);

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task RemoveAsync(string key, int db = -1);

        /// <summary>
        /// 获取Hash值
        /// </summary>
        /// <param name="key">hash表的key</param>
        /// <param name="hashKey">hash key（行key）</param>
        /// <param name="db">db</param>
        /// <returns></returns>
        Task<string> HGetAsync(string key, string hashKey, int db = -1);

        /// <summary>
        /// 获取Hash值
        /// </summary>
        /// <param name="key">hash表的key</param>
        /// <param name="hashKey">hash key（行key）</param>
        /// <param name="db">db</param>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        Task<TResult> HGetAsync<TResult>(string key, string hashKey, int db = -1);

        /// <summary>
        /// 获取Hash表所有值
        /// </summary>
        /// <param name="key">hash表的key</param>
        /// <param name="db">db</param>
        /// <returns></returns>
        Task<HashEntry[]> HGetAllAsync(string key, int db = -1);

        /// <summary>
        /// 判断Hash表中是否存在某个key
        /// </summary>
        /// <param name="key">hash表的key</param>
        /// <param name="hashKey">hash key（行key）</param>
        /// <param name="db">db</param>
        /// <returns></returns>
        Task<bool> HExistsAsync(string key,string hashKey, int db = -1);

        /// <summary>
        /// 获取Hash表中所有的key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<RedisValue[]> HGetKeysAsync(string key, int db = -1);

        /// <summary>
        /// 获取Hash表中所有的value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<RedisValue[]> HGetValuesAsync(string key, int db = -1);

        /// <summary>
        /// 设置Hash值
        /// </summary>
        /// <param name="key">hash表的key</param>
        /// <param name="hashKey">hash key（行key）</param>
        /// <param name="itemValue">item value（行value）</param>
        /// <param name="when">在什么条件下执行操作</param>
        /// <param name="db">db</param>
        /// <returns></returns>
        Task<bool> HSetAsync(string key, string hashKey, string itemValue, When when = When.Always, int db = -1);

        /// <summary>
        /// 设置Hash值
        /// </summary>
        /// <param name="key">hash表的key</param>
        /// <param name="hashKey">hash key（行key）</param>
        /// <param name="itemValue">item value（行value）</param>
        /// <param name="when">在什么条件下执行操作</param>
        /// <param name="db">db</param>
        /// <typeparam name="TParams"></typeparam>
        /// <returns></returns>
        Task<bool> HSetAsync<TParams>(string key, string hashKey, TParams itemValue, When when = When.Always, int db = -1);

        /// <summary>
        /// 删除Hash值
        /// </summary>
        /// <param name="key">hash表的key</param>
        /// <param name="hashKey">hash key（行key）</param>
        /// <param name="db">db</param>
        /// <returns></returns>
        Task<bool> HRemoveAsync(string key, string hashKey, int db = -1);

        /// <summary>
        /// 递增值
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">递增值，默认1</param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<long> IncrementAsync(string key, long value = 1L, int db = -1);

        /// <summary>
        /// 递减值
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">递减值，默认1</param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<long> DecrementAsync(string key, long value = 1L, int db = -1);

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        Task<long> ListLeftPushAsync(string redisKey, string redisValue, int db = -1);

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        Task<long> ListRightPushAsync(string redisKey, string redisValue, int db = -1);

        /// <summary>
        /// 在列表尾部插入数组集合。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        Task<long> ListRightPushAsync(string redisKey, IEnumerable<string> redisValue, int db = -1);

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<T> ListLeftPopAsync<T>(string redisKey, int db = -1) where T : class;

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<T> ListRightPopAsync<T>(string redisKey, int db = -1) where T : class;

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<string> ListLeftPopAsync(string redisKey, int db = -1);

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<string> ListRightPopAsync(string redisKey, int db = -1);

        /// <summary>
        /// 获取列表长度
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<long> ListLengthAsync(string redisKey, int db = -1);

        /// <summary>
        /// 返回在该列表上键所对应的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> ListRangeAsync(string redisKey, int db = -1);

        /// <summary>
        /// 根据索引获取指定位置数据
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> ListRangeAsync(string redisKey, int start, int stop, int db = -1);

        /// <summary>
        /// 删除List中的元素 并返回删除的个数
        /// </summary>
        /// <param name="redisKey">key</param>
        /// <param name="redisValue">元素</param>
        /// <param name="type">大于零 : 从表头开始向表尾搜索(只删除匹配到的第一个)。小于零 : 从表尾开始向表头搜索(只删除匹配到的第一个)。等于零：移除表中所有与 VALUE 相等的值</param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<long> ListDelRangeAsync(string redisKey, string redisValue, long type = 0, int db = -1);

        /// <summary>
        /// 清空List
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="db"></param>
        Task ListClearAsync(string redisKey, int db = -1);
    }
}
