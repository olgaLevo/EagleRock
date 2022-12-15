using EagleRock.Api.Data.Interfaces;
using EagleRock.Api.DomainModels;

namespace EagleRock.Api.Data
{
    public class EagleBotRecordValidator : IEagleBotRecordValidator
    {
        public ValidationResult ValidateNewEagleBootRecord(EagleBotRecord record)
        {
            _ = record ?? throw new ArgumentNullException(nameof(record), "A record is required");
            var result = new ValidationResult();

            if (record.BotId == default)
                result.Errors.Add(nameof(record.BotId), "A non-default ID is required.");

            if (string.IsNullOrEmpty(record.RecordKey))
                result.Errors.Add(nameof(record.RecordKey), "A Record Key is required.");

            if (string.IsNullOrEmpty(record.Location))
                result.Errors.Add(nameof(record.Location), "The Location field is required.");

            if (record.Location.Length > 256)
                result.Errors.Add(nameof(record.Location), "The field Location must be a string with a maximum length of 256.");

            if (record.AverageVehicleSpeed > 200)
                result.Errors.Add(nameof(record.AverageVehicleSpeed), "An Average Vehicle Speed acceeds the limit");

            if (string.IsNullOrEmpty(record.RoadName))
                result.Errors.Add(nameof(record.RoadName), "The RoadName field is required.");

            return result;

        }
    }
}
