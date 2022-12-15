namespace EagleBot.Api.Tests.Models
{
    public class TestEagleBotInputModel
    {
        public Guid Id { get; set; }

        public string Location { get; set; }
        public DateTime Time { get; set; }
        public string RoadName { get; set; }
        public string Status { get; set; }
        public string TrafficFlowDirection { get; set; }
        public int TrafficFlowRate { get; set; }
        public int AverageVehicleSpeed { get; set; }

        public TestEagleBotInputModel CloneWith(Action<TestEagleBotInputModel> changes)
        {
            var clone = (TestEagleBotInputModel)MemberwiseClone();

            changes(clone);

            return clone;
        }
    }
}
