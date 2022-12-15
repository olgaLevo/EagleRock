using EagleRock.Api.Data.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text.Json;

namespace EagleRock.Api.Data
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache _cache;
        private readonly IConfiguration _configuration;

        public RedisCacheService(IDistributedCache cache, IConfiguration configuration)
        {
            _cache = cache;
            _configuration = configuration;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var value = await _cache.GetStringAsync(key);

            if (value != null)
            {
                return JsonSerializer.Deserialize<T>(value);
            }

            return default;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var keys = GetAllkeys();
            if (keys != null && keys.Any())
            {
                var stringItems = new List<string>();
                foreach (var key in keys)
                {
                    var item = await _cache.GetStringAsync(key);
                    if (item != null)
                        stringItems.Add(item);
                }

                return stringItems.Select(m => JsonSerializer.Deserialize<T>(m));
            }

            return default;
        }

        public async Task RemoveAsync(string key)
        {
            var keyExists = await _cache.GetStringAsync(key);
            if (keyExists != null)
                await _cache.RemoveAsync(key);
        }

        public async Task<T> SetAsync<T>(string key, T value)
        {
            var timeOut = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
            };

            var response = JsonSerializer.Serialize(value);

            await _cache.SetStringAsync(key, response, timeOut);

            return value;
        }

        private List<string> GetAllkeys()
        {
            List<string> listKeys = new List<string>();
            var connectionString = _configuration.GetSection("Redis")["ConnectionString"];
            var host = connectionString.Split(':')[0];
            var port = connectionString.Split(':')[1] != null ? Convert.ToInt32(connectionString.Split(':')[1]) : 0;
            using (var redis = ConnectionMultiplexer.Connect($"{connectionString}"))
            {
                var keys = redis.GetServer(host, port).Keys();
                listKeys.AddRange(keys.Select(key => (string)key).ToList());
            }

            return listKeys;
        }
    }
}
