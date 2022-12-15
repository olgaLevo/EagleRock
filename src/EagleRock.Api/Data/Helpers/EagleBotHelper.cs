using EagleRock.Api.DomainModels;
using EagleRock.Api.Models.Output;

namespace EagleRock.Api.Data.Helpers
{
    public static class EagleBotHelper
    {
        public static EagleBotOutputModel[] ToEagleBotOutputModel(IEnumerable<EagleBotRecord> records)
        {
            if (records == null)
                return null;


            var outputModel = records.GroupBy(m => m.BotId)
                .Select(n =>
                new EagleBotOutputModel()
                {
                    Id = n.Key,
                    CurrentLocation = n.OrderByDescending(x => x.Time).FirstOrDefault().Location,
                    Status = n.OrderByDescending(x => x.Time).FirstOrDefault().Status,
                    TrafficDataRecords = n.Select(c =>
                    new TrafficDataOutputModel()
                    {
                        AverageVehicleSpeed = c.AverageVehicleSpeed,
                        Location = c.Location,
                        RoadName = c.RoadName,
                        Time = c.Time,
                        TrafficFlowDirection = c.TrafficFlowDirection,
                        TrafficFlowRate = c.TrafficFlowRate

                    }).ToList()
                }).ToArray();

            return outputModel;
        }
    }
}
