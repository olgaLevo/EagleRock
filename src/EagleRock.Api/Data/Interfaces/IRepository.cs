namespace EagleRock.Api.Data.Interfaces
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Adds data into repository asynchronously
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<AddRecordResult> AddAsync(T entity);

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyCollection<T>> GetAllAsync(DateTime? from = null, DateTime? to = null, bool cachedOnly = true);
    }
}
