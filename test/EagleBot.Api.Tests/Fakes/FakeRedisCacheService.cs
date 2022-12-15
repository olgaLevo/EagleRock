using EagleRock.Api.Data;
using EagleRock.Api.Data.Dto;
using EagleRock.Api.Data.Interfaces;
using EagleRock.Api.DomainModels;
using System.Text.Json;

namespace EagleBot.Api.Tests.Fakes
{
    public class FakeRedisCacheService : IRedisCacheService
    {
        private IReadOnlyCollection<EagleBotDto> _eagleBotRecords;

        public List<EagleBotDto> Records { get; set; }

        public void ResetDefaultRecords(bool useCustomIfAvailable = true)
        {
            Records = _eagleBotRecords is object && useCustomIfAvailable
                ? _eagleBotRecords.ToList()
                : GetDefaultRecords();
        }
        public static FakeRedisCacheService WithDefaultRecords()
        {
            var repo = new FakeRedisCacheService();
            repo.ResetDefaultRecords();
            return repo;
        }

        private List<EagleBotDto> GetDefaultRecords() => new List<EagleBotDto>
        {
            new EagleBotDto
            {
                BotId = new Guid("51185BC3-593F-4141-AAB0-F7F4679B59F3"),
                Location = "-26.564735, 153.084453",
                RecordKey = $"51185BC3-593F-4141-AAB0-F7F4679B59F3-{DateTime.Now.AddHours(-1).ToLongTimeString()}",
                RoadName = "David Low way",
                Status = "Active",
                Time = DateTime.Now.AddHours(-1),
                AverageVehicleSpeed = 40,
                TrafficFlowDirection = "south",
                TrafficFlowRate = 8
            },
            new EagleBotDto
            {
                BotId = new Guid("10100F1D-9B02-46C9-8528-478B2BCBD7B1"),
                Location = "-26.827507, 153.032187",
                RecordKey = $"10100F1D-9B02-46C9-8528-478B2BCBD7B1-{DateTime.Now.AddMinutes(-1).ToLongTimeString()}",
                RoadName = "David Low way",
                Status = "Active",
                Time = DateTime.Now.AddMinutes(-1),
                AverageVehicleSpeed = 42,
                TrafficFlowDirection = "north",
                TrafficFlowRate = 12
            },
            new EagleBotDto
            {
                BotId = new Guid("10100F1D-9B02-46C9-8528-478B2BCBD7B1"),
                Location = "-26.827507, 153.032187",
                RecordKey = $"10100F1D-9B02-46C9-8528-478B2BCBD7B1-{DateTime.Now.AddMinutes(-10).ToLongTimeString()}",
                RoadName = "David Low way",
                Status = "Active",
                Time = DateTime.Now.AddMinutes(-10),
                AverageVehicleSpeed = 45,
                TrafficFlowDirection = "north",
                TrafficFlowRate = 4
            },
            new EagleBotDto
            {
                BotId = new Guid("10100F1D-9B02-46C9-8528-478B2BCBD7B1"),
                Location = "-26.827507, 153.032187",
                RecordKey = $"10100F1D-9B02-46C9-8528-478B2BCBD7B1-{DateTime.Now.AddMinutes(-10).ToLongTimeString()}",
                RoadName = "David Low way",
                Status = "Active",
                Time = DateTime.Now.AddMinutes(-10),
                AverageVehicleSpeed = 120,
                TrafficFlowDirection = "north",
                TrafficFlowRate = 4
            }
        };





        public Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return Task.FromResult(Records as IEnumerable<T>);
        }

        public Task<T> SetAsync<T>(string key, T value)
        {
            if (value is EagleBotDto eagleBotRecord)
            {
                Records.Add(eagleBotRecord);
            }
            return default;
        }

        //not implemented methods
        public Task<T> GetAsync<T>(string key)
        {
            return Task.FromResult((T) Convert.ChangeType((Records.FirstOrDefault(m => m.BotId.ToString() == key) ), typeof(T)));
        }
        public Task RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }
    }
}
