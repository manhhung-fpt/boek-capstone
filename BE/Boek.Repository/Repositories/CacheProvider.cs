using Boek.Core.Extensions;
using Boek.Repository.Interfaces;
using StackExchange.Redis;

namespace Boek.Repository.Repositories
{
    public class CacheProvider : ICacheProvider
    {
        private readonly IDatabase redis;
        public TimeSpan Expire { set; get; }

        public CacheProvider(IConnectionMultiplexer connectionMultiplexer)
        {
            redis = connectionMultiplexer.GetDatabase();
            Expire = TimeSpan.MaxValue;
        }

        public async Task<T> GetValueAsync<T>(string key)
        {
            string value = await redis.StringGetAsync(key);

            return value != null ? value.ToObject<T>() : default;
        }

        public bool IsExist(string key) => redis.KeyExists(key);

        public async Task<bool> IsExistAsync(string key) => await redis.KeyExistsAsync(key);

        public bool Remove(string key) => redis.KeyDelete(key);

        public async Task<bool> RemoveAsync(string key) => await redis.KeyDeleteAsync(key);

        public bool SetValue(string key, object value) => redis.StringSet(key, value.ToJson(), Expire);

        public async Task<bool> SetValueAsync(string key, object value) => await redis.StringSetAsync(key, value.ToJson(), Expire);

        public T GetValue<T>(string key)
        {
            string value = redis.StringGet(key);

            return value != null ? value.ToObject<T>() : default;
        }
    }
}