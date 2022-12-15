using EagleBot.Api.Tests.Fakes;
using EagleRock.Api.BackgroundServices;
using EagleRock.Api.Data.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace EagleBot.Api.Tests
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {

        public FakeRedisCacheService FakeRedisCacheService { get; }

        public CustomWebApplicationFactory()
        {
            FakeRedisCacheService = FakeRedisCacheService.WithDefaultRecords();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IRedisCacheService>(FakeRedisCacheService);
               
             
            });
        }
    }
}
