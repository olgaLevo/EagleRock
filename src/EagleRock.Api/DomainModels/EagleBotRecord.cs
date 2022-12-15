namespace EagleRock.Api.DomainModels
{
    public class EagleBotRecord
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
    }
}
