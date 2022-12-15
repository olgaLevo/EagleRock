using EagleRock.Api.Data;
using EagleRock.Api.Data.Extensions;
using EagleRock.Api.Data.Interfaces;

namespace EagleRock.Api.BackgroundServices
{
    public class SeedEagleService : IHostedService
    {

        private readonly IRedisCacheService _cache;
        private readonly ILogger<SeedEagleService> _logger;
        private readonly IHostEnvironment _environment;

        public SeedEagleService(IRedisCacheService cache, ILogger<SeedEagleService> logger, IHostEnvironment environment)
        {
            _cache = cache;
            _logger = logger;
            _environment = environment;
        }


        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (_environment.IsProduction())
                return; // don't add default products in production

            try
            {
                if (_cache is RedisCacheService service)
                    await service.InitialiseDefaultRecords();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialise the Eagle Boot Data Repository.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
