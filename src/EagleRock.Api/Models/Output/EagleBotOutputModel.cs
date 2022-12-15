namespace EagleRock.Api.Models.Output
{
    public class EagleBotOutputModel
    {
        public Guid Id { get; set; }
        public string CurrentLocation { get; set; }
        public string Status { get; set; }
        public IReadOnlyCollection<TrafficDataOutputModel> TrafficDataRecords { get; set; }
    }
    public class TrafficDataOutputModel
    {
        public DateTime Time { get; set; }
        public string Location { get; set; }
        public string RoadName { get; set; }
        public string TrafficFlowDirection { get; set; }
        public int TrafficFlowRate { get; set; }
        public int AverageVehicleSpeed { get; set; }
    }
}
