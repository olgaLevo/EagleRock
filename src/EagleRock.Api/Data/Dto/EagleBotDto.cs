using EagleRock.Api.DomainModels;

namespace EagleRock.Api.Data.Dto
{
    public class EagleBotDto
    {
        public Guid BotId { get; set; }
        /// <summary>
        /// Key used as a unique identifier for the record. Only one record per milisec
        /// </summary>
        public string RecordKey { get; set; }

        public string Location { get; set; }
        public string Status { get; set; }
        public DateTime Time { get; set; }
        public string RoadName { get; set; }
        public string TrafficFlowDirection { get; set; }
        public int TrafficFlowRate { get; set; }
        public int AverageVehicleSpeed { get; set; }

        public static EagleBotDto FromEagleBotRecord(EagleBotRecord record) => new EagleBotDto
        {
            BotId = record.BotId,
            RecordKey = record.RecordKey,
            Status = record.Status,
            Location = record.Location,
            Time = record.Time,
            RoadName = record.RoadName,
            TrafficFlowDirection = record.TrafficFlowDirection,
            TrafficFlowRate = record.TrafficFlowRate,
            AverageVehicleSpeed = record.AverageVehicleSpeed
        };

        public EagleBotRecord ToEagleBotRecord()
        {
            return new EagleBotRecord()
            {
                BotId = BotId,
                RecordKey = RecordKey,
                Location = Location,
                Time = Time,
                Status = Status,
                RoadName = RoadName,
                TrafficFlowDirection = TrafficFlowDirection,
                TrafficFlowRate = TrafficFlowRate,
                AverageVehicleSpeed = AverageVehicleSpeed
            };
        }
    }
}
