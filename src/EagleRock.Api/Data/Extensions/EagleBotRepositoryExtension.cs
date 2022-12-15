using EagleRock.Api.Data.Dto;

namespace EagleRock.Api.Data.Extensions
{
    public static class EagleBotRepositoryExtension
    {
        // initialise records which would normally already exist in a real provider database
        public static async Task InitialiseDefaultRecords(this RedisCacheService service)
        {
            var recordsToInsert = new List<EagleBotDto>
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

            foreach (var record in recordsToInsert)
            {
                await service.SetAsync<EagleBotDto>(record.RecordKey, record);
            }

        }
    }
}
