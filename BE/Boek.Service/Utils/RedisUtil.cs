using Boek.Repository.Interfaces;

namespace Boek.Service.Utils
{
    public class RedisUtil
    {
        public static T GetDataFromCache<T>(ICacheProvider cacheProvider, string key)
        {
            if (cacheProvider.IsExist(key))
                return cacheProvider.GetValue<T>(key);
            return default(T);
        }
    }
}