using EagleRock.Api.Data.Dto;
using EagleRock.Api.Data.Interfaces;
using EagleRock.Api.DomainModels;

namespace EagleRock.Api.Data
{
    public class EagleBotRepository : IEagleBotRepository
    {
        private readonly DbContextClass _dbContext;
        private readonly IRedisCacheService _redisCacheService;
        private readonly IEagleBotRecordValidator _eagleBootRecordValidator;

        public EagleBotRepository(DbContextClass dbContext,
            IRedisCacheService redisCacheService,
            IEagleBotRecordValidator eagleBootRecordValidator)
        {
            _dbContext = dbContext;
            _redisCacheService = redisCacheService;
            _eagleBootRecordValidator = eagleBootRecordValidator;
        }

        public async Task<AddRecordResult> AddAsync(EagleBotRecord entity)
        {
            _ = entity ?? throw new ArgumentNullException(nameof(entity), "A record is required");

            var validationResult = _eagleBootRecordValidator.ValidateNewEagleBootRecord(entity); //exclude items with the same timestamp from the same bot          

            if (validationResult.IsValid)
            {
                var cachedItem = await _redisCacheService.GetAsync<EagleBotRecord>(entity.BotId.ToString());
                if (cachedItem != null)
                {
                    var dto = EagleBotDto.FromEagleBotRecord(entity);
                    await _redisCacheService.SetAsync<EagleBotDto>(entity.BotId.ToString(), dto);
                    await _dbContext.AddAsync<EagleBotDto>(dto);
                }

                return new AddRecordResult(validationResult, false);
            }
            return new AddRecordResult(validationResult, validationResult.IsValid);
        }

        public async Task<IReadOnlyCollection<EagleBotRecord>> GetAllAsync(DateTime? from = null, DateTime? to = null, bool cachedOnly = true)
        {
            var allCached = await _redisCacheService.GetAllAsync<EagleBotDto>();

            if (cachedOnly && allCached != default)
            {
                return allCached.Select(m => m.ToEagleBotRecord()).ToArray();
            }

            if (from == null)
                from = DateTime.MinValue;
            if (to == null)
                to = DateTime.Now;

            if (!cachedOnly)
            {
                if (allCached != default)
                {
                    var dbRecords = _dbContext.EagleBootRecords.Where(m => !allCached.Any(n => n.RecordKey == m.RecordKey) && m.Time > from && m.Time > to).ToArray();
                    var result = allCached.Select(m => m.ToEagleBotRecord()).ToList();
                    if (dbRecords.Length > 0)
                        result.AddRange(dbRecords.Select(m => m.ToEagleBotRecord()));
                    return result.ToArray();
                }
                else
                {
                    var dbRecords = _dbContext.EagleBootRecords.Where(m => m.Time > from && m.Time > to).ToArray();
                    return dbRecords.Select(m => m.ToEagleBotRecord()).ToArray();
                }
            }
            return null;
        }
    }
}
