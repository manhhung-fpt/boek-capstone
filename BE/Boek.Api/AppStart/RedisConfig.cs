using Boek.Repository.Interfaces;
using Boek.Repository.Repositories;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;
using StackExchange.Redis;
using Boek.Core.Constants;

namespace Boek.Api.AppStart
{
    public static class RedisConfig
    {
        public static IServiceCollection ConfigureStackExchangeRedis(this IServiceCollection services, IConfiguration config)
        {
            string redisConnectString = config.GetConnectionString(MessageConstants.REDIS);

            services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(new RedisConfiguration { ConnectionString = redisConnectString });
            services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(redisConnectString));
            services.AddScoped<ICacheProvider, CacheProvider>();

            return services;
        }
    }
}