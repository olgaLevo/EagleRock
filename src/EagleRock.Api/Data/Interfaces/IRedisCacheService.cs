namespace EagleRock.Api.Data.Interfaces
{
    public interface IRedisCacheService
    {
        /// <summary>
        /// Get data using the key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key);

        /// <summary>
        /// Get all cached items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync<T>();

        /// <summary>
        /// Set data with Value and Expiration Time of Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirationTime"></param>
        /// <returns></returns>
        Task<T> SetAsync<T>(string key, T value);

        /// <summary>
        /// Remove Data
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task RemoveAsync(string key);
    }
}
