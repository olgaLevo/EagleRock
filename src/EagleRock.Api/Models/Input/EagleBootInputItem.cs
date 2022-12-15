using EagleRock.Api.DomainModels;
using System.ComponentModel.DataAnnotations;

namespace EagleRock.Api.Models.Input
{
    public class EagleBootInputItem
    {
        [Required]
        public Guid Id { get; set; }

        [Required, StringLength(256)]
        public string Location { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public string RoadName { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string TrafficFlowDirection { get; set; }
        [Required]
        public int TrafficFlowRate { get; set; }
        [Required, Range(0, 200)]
        public int AverageVehicleSpeed { get; set; }

        public EagleBotRecord ToEagleBootRecord()
        {
            return new EagleBotRecord
            {
                BotId = Id,
                RecordKey = $"{Id.ToString()}-{Time.ToLongTimeString()}",
                Location = Location,
                Status = Status,
                Time = Time,
                RoadName = RoadName,
                TrafficFlowDirection = TrafficFlowDirection,
                AverageVehicleSpeed = AverageVehicleSpeed,
                TrafficFlowRate = TrafficFlowRate
            };
        }
    }
}
